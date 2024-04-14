import { object, string } from "yup";
import { UpdateProjectPhases, type Phase } from "../api/projectApi";

export function usePhaseForm(initialPhases: Phase[], projectId: string) {
  const show = ref(false);

  const phases = ref<Phase[]>(initialPhases.map(e => {
    return {
      ...e
    }
  }));

  function refresh(newPhases: Phase[]) {
    phases.value = [...newPhases];
  }

  function revert() {
    phases.value = [...initialPhases];
  }

  const model = reactive({
    name: '',
    startDate: '',
    endDate: '',
    description: '',
  });

  const schema = object({
    name: string().required('Name is required').test(
      'name-unique',
      'Phase name is already used',
      (value) => phases.value.some(e => e.name === value)
    ),
    startDate: string().required('Start date is required'),
    endDate: string().required('End date is required'),
    description: string().required('End date is required'),
  });

  function setModel(newModel: Phase) {
    model.name = newModel.name;
    model.startDate = newModel.startDate;
    model.endDate = newModel.endDate;
    model.description = newModel.description;
  }

  function remove(phase: Phase) {
    const index = phases.value.findIndex(e => e.name === phase.name);
    if (index > -1)
      phases.value.splice(index, 1);
  }

  function onSubmit() {
    const foundPhase = phases.value.find(e => e.name === model.name);

    if (!foundPhase)
      return;

    foundPhase.name = model.name;
    foundPhase.startDate = model.startDate;
    foundPhase.endDate = model.endDate;
    foundPhase.description = model.description;

    show.value = false;
    return;
  }
  
  function onCreate() {
    phases.value.push({
      id: undefined,
      ...model
    });

    show.value = false;
  }

  function closeForm() {
    show.value = false;
  }

  function showForm() {
    show.value = true;
  }

  const isSubmitting = ref(false);

  async function persist() {
    const result = await UpdateProjectPhases({
      projectId: projectId,
      phases: phases.value
    });

    return result;
  }
  return {
    show,
    model,
    schema,
    onSubmit,
    setModel,
    showForm,
    closeForm,
    phases,
    persist,
    remove,
    refresh,
    revert
  }
}

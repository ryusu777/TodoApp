import { object, string } from "yup";
import type { Phase } from "../api/projectApi";

export function usePhaseForm(initialPhases: Phase[]) {
  const show = ref(false);

  const phases = ref<Phase[]>(initialPhases);

  const model = reactive({
    name: '',
    startDate: '',
    endDate: '',
    description: '',
  });

  const schema = object({
    name: string().required('Name is required'),
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

  function onSubmit() {
    closeForm();
    return;
  }

  function closeForm() {
    show.value = false;
  }

  function showForm() {
    show.value = true;
  }

  return {
    show,
    model,
    schema,
    onSubmit,
    setModel,
    showForm,
    closeForm
  }
}

import { array, object, string } from "yup";
import { CreateAssignment, UpdateAssignment, type Assignment } from "../api/assignmentApi";

export function useAssignmentForm(projectId: string, subdomainId: string) {
  const show = ref(false);

  const model = reactive({
    id: undefined as string | undefined,
    description: '',
    title: '',
    phaseId: '',
    assignees: [''],
  });

  const schema = object({
    title: string().required('Title is required'),
    phaseId: string().required('Phase is required'),
    subdomainId: string().required('Subdomain is required'),
  });

  function update(newModel: Assignment) {
    model.title = newModel.title;
    model.description = newModel.description || '';
    model.id = newModel.id || '';
    model.phaseId = newModel.phaseId;
    model.assignees = newModel.assignees;

    show.value = true;
  }

  function create() {
    model.title = '';
    model.description = '';
    model.id = undefined;
    model.phaseId = '';
    model.assignees = ['']

    show.value = true;
  }

  function closeForm() {
    show.value = false;
  }

  const isSubmitting = ref(false);

  async function submit() {
    isSubmitting.value = true;
    if (!model.id) {
      const response = await CreateAssignment({
        title: model.title,
        description: model.description,
        phaseId: model.phaseId,
        projectId,
        subdomainId
      });

      isSubmitting.value = false;
      return response?.errorDescription;
    }

    const response = await UpdateAssignment({
      assignmentId: model.id || '',
      title: model.title,
      description: model.description
    });

    isSubmitting.value = false;
    return response?.errorDescription;
  }

  return {
    show,
    model,
    schema,
    closeForm,
    submit,
    create,
    update,
    isSubmitting
  }
}

import { array, object, string } from "yup";
import { CreateAssignment, UpdateAssignment, type Assignment } from "../api/assignmentApi";

export function useAssignmentForm(projectId: string, subdomainId: string) {
  const show = ref(false);

  const model = reactive({
    id: undefined as string | undefined,
    description: '',
    title: '',
    deadline: undefined as string | undefined,
    reviewer: undefined as string | undefined,
    phaseId: undefined as string | undefined,
    subdomainId: undefined as string | undefined,
    assignees: [''],
  });

  const schema = object({
    title: string().required('Title is required'),
  });

  function update(newModel: Assignment) {
    model.id = newModel.id || '';
    model.description = newModel.description || '';
    model.title = newModel.title;
    model.deadline = newModel.deadline;
    model.reviewer = newModel.reviewer;
    model.phaseId = newModel.phaseId;
    model.subdomainId = newModel.subdomainId;
    model.assignees = newModel.assignees;

    show.value = true;
  }

  function create() {
    model.id = undefined;
    model.title = '';
    model.description = '';
    model.deadline = undefined;
    model.reviewer = undefined;
    model.phaseId = undefined;
    model.subdomainId = undefined;
    model.assignees = [''];

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
        subdomainId,
        deadline: model.deadline,
        reviewer: model.reviewer
      });

      isSubmitting.value = false;
      return response?.errorDescription;
    }

    const response = await UpdateAssignment({
      assignmentId: model.id || '',
      title: model.title,
      description: model.description,
      subdomainId: model.subdomainId,
      phaseId: model.phaseId,
      deadline: model.deadline,
      reviewer: model.reviewer
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
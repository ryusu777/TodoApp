import { number, object, string } from "yup";
import { CreateAssignment, UpdateAssignment, type Assignment } from "../api/assignmentApi";
import { useAuth } from "~/domain/auth/composables/useAuth";

export function useAssignmentForm(projectId: string) {
  const show = ref(false);
  const auth = useAuth();

  const model = reactive({
    id: undefined as string | undefined,
    description: '',
    title: '',
    deadline: undefined as string | undefined,
    reviewer: auth.getUsername() as string | undefined,
    phaseId: undefined as string | undefined,
    subdomainId: undefined as string | undefined,
    giteaRepositoryId: undefined as number | undefined,
    assignees: [] as string[],
  });

  const schema = object({
    title: string().required('Title is required'),
    giteaRepositoryId: number().required('Please select repository'),
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
    model.giteaRepositoryId = -1;

    show.value = true;
  }

  function create(subdomainId: string) {
    model.id = undefined;
    model.title = '';
    model.description = '';
    model.deadline = undefined;
    model.reviewer = undefined;
    model.phaseId = undefined;
    model.subdomainId = subdomainId;
    model.giteaRepositoryId = undefined;
    model.assignees = [];

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
        subdomainId: model.subdomainId,
        deadline: model.deadline,
        reviewer: model.reviewer,
        assignees: model.assignees,
        giteaRepositoryId: model.giteaRepositoryId as number,
        projectId
      });

      isSubmitting.value = false;
      return response?.errorDescription;
    }

    const response = await UpdateAssignment({
      assignmentId: model.id || '',
      title: model.title,
      description: model.description,
      assignees: model.assignees,
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
    isSubmitting,
    projectId
  }
}

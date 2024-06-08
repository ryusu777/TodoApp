import { object, string } from "yup";
import { CreateProjectHierarchy, UpdateProjectHierarchyDetail, type Hierarchy } from "~/domain/project/api/projectApi";

export function useHierarchyForm(projectId: string) {
  const show = ref(false);

  const model = reactive({
    id: undefined as string | undefined,
    name: '',
    superiorHierarchyId: undefined as string | undefined
  });

  const schema = object({
    name: string().required('Title is required'),
  });

  function update(newModel: Hierarchy) {
    model.id = newModel.id;
    model.name = newModel.name;
    model.superiorHierarchyId = newModel.superiorHierarchyId;

    show.value = true;
  }

  function create() {
    model.name = '';
    model.superiorHierarchyId = undefined;
    model.id = undefined;

    show.value = true;
  }

  function closeForm() {
    show.value = false;
  }

  const isSubmitting = ref(false);

  async function onSubmit() {
    isSubmitting.value = true;
    if (!model.id) {
      const response = await CreateProjectHierarchy({
        projectId,
        name: model.name,
        superiorHierarchyId: model.superiorHierarchyId,
        memberUsernames: []
      });

      isSubmitting.value = false;
      return response?.errorDescription;
    }

    const response = await UpdateProjectHierarchyDetail({
      projectId,
      hierarchyId: model.id,
      name: model.name,
      superiorHierarchyId: model.superiorHierarchyId
    });

    isSubmitting.value = false;
    return response?.errorDescription;
  }
  // end

  return {
    show,
    model,
    schema,
    update,
    create,
    closeForm,
    isSubmitting,
    onSubmit
  };
}

import { object, string } from "yup";
import { CreateSubdomain, UpdateSubdomain, type Subdomain } from "../api/subdomainApi";

export function useSubdomainForm(projectId: string) {
  const show = ref(false);

  const model = reactive({
    subdomainId: '' as string | undefined,
    description: '',
    title: ''
  });

  const schema = object({
    title: string().required('Title is required'),
    description: string().required('Description is required')
  });

  function update(newModel: Subdomain) {
    model.title = newModel.title;
    model.description = newModel.description;
    model.subdomainId = newModel.id;

    show.value = true;
  }

  function create() {
    model.title = '';
    model.description = '';
    model.subdomainId = undefined;

    show.value = true;
  }

  function closeForm() {
    show.value = false;
  }

  const isSubmitting = ref(false);

  async function submit() {
    isSubmitting.value = true;
    if (!model.subdomainId) {
      const response = await CreateSubdomain({
        title: model.title,
        description: model.description,
        projectId: projectId
      });

      isSubmitting.value = false;
      return response?.errorDescription;
    }

    const response = await UpdateSubdomain({
      subdomainId: model.subdomainId || '',
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

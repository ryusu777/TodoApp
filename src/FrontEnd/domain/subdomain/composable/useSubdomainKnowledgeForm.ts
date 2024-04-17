import { object, string } from "yup";
import { CreateSubdomainKnowledge, UpdateSubdomainKnowledge, type SubdomainKnowledge } from "../api/subdomainApi";

export function useSubdomainKnowledgeForm(subdomainId: string) {
  const show = ref(false);

  const model = reactive({
    knowledgeId: '' as string | undefined,
    content: '',
    title: ''
  });

  const schema = object({
    title: string().required('Title is required'),
  });

  async function update(newModel: SubdomainKnowledge) {
    isSubmitting.value = true;
    const response = await UpdateSubdomainKnowledge({
      subdomainId: subdomainId,
      subdomainKnowledgeId: newModel.id || "",
      title: newModel.title,
      content: newModel.content
    });
    isSubmitting.value = false;
  }

  function create() {
    model.title = '';
    model.content = '';
    model.knowledgeId = undefined;

    show.value = true;
  }

  function closeForm() {
    show.value = false;
  }

  const isSubmitting = ref(false);

  async function submit() {
    isSubmitting.value = true;
    const response = await CreateSubdomainKnowledge({
      title: model.title,
      content: model.content,
      subdomainId: subdomainId
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

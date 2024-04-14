import { object, string } from "yup";
import { UpdateProjectMembers, type Member } from "../api/projectApi";

export function useMemberForm(initialMembers: Member[], projectId: string) {
  const show = ref(false);

  let original = initialMembers;

  const members = ref<Member[]>(original.map(e => {
    return {
      ...e
    };
  }));

  const currentEditIndex = ref(-1);

  function refresh(newMembers: Member[]) {
    members.value = [...newMembers];
    original = newMembers;
  }

  function revert() {
    members.value = [...original];
  }

  const model = reactive({
    username: '',
  });

  const schema = object({
    username: string().required('Username is required').test(
      'unique-name',
      'User is already registered',
      (value) => {
        const foundIndex = members.value.findIndex(e => e.username === value);
        if (foundIndex === -1)
          return true;

        if (foundIndex === currentEditIndex.value)
          return true;

        return false;
      }
    ),
  });

  function setModel(newModel: Member) {
    currentEditIndex.value = members.value.findIndex(e => e.username === newModel.username);
    model.username = newModel.username;
  }

  function remove(member: Member) {
    const index = members.value.findIndex(e => e.username === member.username);
    if (index > -1)
      members.value.splice(index, 1);
  }

  function onSubmit() {
    if (currentEditIndex.value === -1) {
      members.value.push({
        ...model
      });
      show.value = false;
      return;
    }
    const foundPhase = members.value.find(e => e.username === model.username);

    if (!foundPhase)
      return;

    foundPhase.username = model.username;

    show.value = false;
    return;
  }
  
  function onCreate() {
    members.value.push({
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
    const result = await UpdateProjectMembers({
      projectId: projectId,
      memberUsernames: members.value.map(e => e.username)
    });

    original = members.value.map(e => {
      return {
        ...e
      };
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
    members,
    persist,
    remove,
    refresh,
    revert
  }
}

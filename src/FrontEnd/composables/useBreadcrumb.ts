import type { RouteRecordName, Router } from "vue-router";

type Link = {
  label: RouteRecordName | undefined;
  icon?: string;
  to?: string;
};

export const useBreadcrumb = defineStore('useBreadcrumb', () => {
  const converter: {
    key: string,
    label: ComputedRef<RouteRecordName | undefined>
  }[] = [];

  function setConverter(key: string, labelCallback: ComputedRef<string | undefined>) {
    const foundConverter = converter.find(e => e.key === key);
    if (foundConverter) {
      foundConverter.label = labelCallback;
      return;
    }

    converter.push({
      key,
      label: labelCallback
    });
  }

  const links = ref<Link[]>([
    {
      label: 'Home',
      icon: 'i-heroicons-home',
      to: '/'
    }
  ]);
  const router = useRouter();
  const crumb = ref<Link[]>([]);
  const computedLinks = computed(() => links.value.concat(crumb.value));

  function updateRoute(fullRoutePath: string) {
    const params = fullRoutePath.startsWith('/')
      ? fullRoutePath.substring(1).split('/')
      : fullRoutePath.split('/');

    crumb.value = [];

    let path = '';

    params.filter(e => e.length > 0).forEach((param, index) => {
      path = `${path}/${param}`;
      const match = router.resolve(path)

      if (match.name !== null) {
        const foundConverter = converter.find(e => e.key === match.name);

        crumb.value.push({
          label: foundConverter ? foundConverter.label.value : match.name as RouteRecordName,
          to: match.path,
          icon: match.meta.icon as string || "",
        });
      }
    });
  }

  updateRoute(router.currentRoute.value.fullPath);

  return {
    links: computedLinks,
    updateRoute,
    setConverter
  }
})

import type { RouteRecordName, Router } from "vue-router";

type Link = {
  label: RouteRecordName | undefined;
  icon?: string;
  to?: string;
};

const links = ref<Link[]>([]);

export const useBreadcrumb = (router: Router) => {
  function updateRoute(fullRoutePath: string) {
    links.value = [];

    const params = fullRoutePath.startsWith('/')
      ? fullRoutePath.substring(1).split('/')
      : fullRoutePath.split('/');

    const crumbs = [] as Link[];

    let path = '';

    params.filter(e => e.length > 0).forEach((param, index) => {
      path = `${path}/${param}`;
      const match = router.resolve(path)

      if (match.name !== null) {
        crumbs.push({
          label: match.name,
          to: match.path,
          icon: match.meta.icon as string || "",
        });
      }
    })

    links.value = [
      {
        label: 'Home',
        icon: 'i-heroicons-home',
        to: '/'
      },
      ...crumbs
    ];
  }

  updateRoute(router.currentRoute.value.fullPath);

  return {
    links,
    updateRoute
  }
}

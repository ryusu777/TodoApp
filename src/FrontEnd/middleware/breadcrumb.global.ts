export default defineNuxtRouteMiddleware(to => {
  const breadcrumb = useBreadcrumb(useRouter());
  breadcrumb.updateRoute(to.fullPath);
})

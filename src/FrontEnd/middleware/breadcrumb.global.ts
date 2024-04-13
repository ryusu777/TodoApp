export default defineNuxtRouteMiddleware(to => {
  const breadcrumb = useBreadcrumb();
  breadcrumb.updateRoute(to.fullPath);
})

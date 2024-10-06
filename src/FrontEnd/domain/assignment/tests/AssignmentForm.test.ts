// @vitest-environment nuxt

import AssignmentForm from '../components/AssignmentForm.vue';
import { describe, expect, it } from 'vitest';
import { mockNuxtImport, mountSuspended } from '@nuxt/test-utils/runtime';
import { useAssignmentForm } from '../composables/useAssignmentForm';
import { AssignmentFormTestIds } from './test-ids';
import { useProject } from '~/domain/project/composable/useProject';
import { useSubdomainTabs } from '~/domain/subdomain/composable/useSubdomainTabs';

const projectId = 'TodoApp';
const subdomainId = 'random-subdomain-id';

await useProject().fetch(projectId, false);
const subdomainStore = useSubdomainTabs();
await subdomainStore.setProjectId(projectId);
await subdomainStore.fetch(false);

describe('AssignmentForm', () => {
  it('renders form', async () => {
    const form = useAssignmentForm(projectId);
    const wrapper = await mountSuspended(AssignmentForm, {
      props: {
        form,
      }
    });
    // make sure every test id in AssignmentFormTestIds is rendered

    expect(wrapper.find('form').exists()).toBe(true);
    expect(wrapper.find(`[data-testid="${AssignmentFormTestIds.title}"]`).exists()).toBe(true);
    expect(wrapper.find(`[data-testid="${AssignmentFormTestIds.description}"]`).exists()).toBe(true);
    expect(wrapper.find(`[data-testid="${AssignmentFormTestIds.deadline}"]`).exists()).toBe(true);
    expect(wrapper.find(`[data-testid="${AssignmentFormTestIds.reviewer}"]`).exists()).toBe(true);
    expect(wrapper.find(`[data-testid="${AssignmentFormTestIds.phaseId}"]`).exists()).toBe(true);
    expect(wrapper.find(`[data-testid="${AssignmentFormTestIds.subdomainId}"]`).exists()).toBe(true);
    expect(wrapper.find(`[data-testid="${AssignmentFormTestIds.giteaRepositoryId}"]`).exists()).toBe(true);
    expect(wrapper.find(`[data-testid="${AssignmentFormTestIds.assignees}"]`).exists()).toBe(true);
    expect(wrapper.find(`[data-testid="${AssignmentFormTestIds.cancelButton}"]`).exists()).toBe(true);
    expect(wrapper.find(`[data-testid="${AssignmentFormTestIds.submitButton}"]`).exists()).toBe(true);
  });

  it('select phase has options', async () => {
    const form = useAssignmentForm(projectId);
    const wrapper = await mountSuspended(AssignmentForm, {
      props: {
        form,
      }
    });

    await wrapper.find(`[data-testid="${AssignmentFormTestIds.phaseId}"]`).trigger('click');

    const options = wrapper.find('ul').findAll('li');

    expect(options.length).toBeGreaterThan(0);
  });

  it('select subdomain has options', async () => {
    const form = useAssignmentForm(projectId);
    const wrapper = await mountSuspended(AssignmentForm, {
      props: {
        form,
      }
    });

    await wrapper.find(`[data-testid="${AssignmentFormTestIds.subdomainId}"]`).trigger('click');
    const options = wrapper.find('ul').findAll('li');

    expect(options.length).toBeGreaterThan(0);
  });

  it('select repository has correct options', async () => {
    const form = useAssignmentForm(projectId);
    const wrapper = await mountSuspended(AssignmentForm, {
      props: {
        form,
      }
    });

    await wrapper.find(`[data-testid="${AssignmentFormTestIds.giteaRepositoryId}"]`).trigger('click');
    await wrapper.vm.$nextTick();
    const options = wrapper.find('ul').findAll('li');

    expect(options.length).toBeGreaterThan(0);
  });

  it('validates when the form is submitted with valid data', async () => {
    const form = useAssignmentForm(projectId);
    const wrapper = await mountSuspended(AssignmentForm, {
      props: {
        form,
      }
    });

    // fill the form with valid data, only the required fields in form.schema should be filled
    await wrapper.find(`[data-testid="${AssignmentFormTestIds.title}"]`).setValue('Test Title');
    await wrapper.find(`[data-testid="${AssignmentFormTestIds.giteaRepositoryId}"]`).trigger('click');
    await wrapper.find('ul').find('li').trigger('click');

    // submit the form
    await wrapper.find('form').trigger('submit');

    // check if the form is submitted successfully
    expect(wrapper.emitted('submit')).toBeTruthy();
  });

  it('validates when the form is submitted without title', async () => {
    const form = useAssignmentForm(projectId);
    const wrapper = await mountSuspended(AssignmentForm, {
      props: {
        form,
      }
    });

    // fill the form with invalid data, only the required fields in form.schema should be filled
    await wrapper.find(`[data-testid="${AssignmentFormTestIds.title}"]`).setValue('');
    await wrapper.find(`[data-testid="${AssignmentFormTestIds.giteaRepositoryId}"]`).trigger('click');
    await wrapper.vm.$nextTick();
    await wrapper.find('ul').find('li').trigger('click');

    // submit the form
    await wrapper.find('form').trigger('submit');

    // check if the form is not submitted
    expect(wrapper.emitted('submit')).toBeFalsy();
  });


  it('validates when the form is submitted without gitea repositoryId', async () => {
    const form = useAssignmentForm(projectId);
    const wrapper = await mountSuspended(AssignmentForm, {
      props: {
        form,
      }
    });

    // fill the form with invalid data, only the required fields in form.schema should be filled
    await wrapper.find(`[data-testid="${AssignmentFormTestIds.title}"]`).setValue('Test Title');

    // submit the form
    await wrapper.find('form').trigger('submit');

    // check if the form is not submitted
    expect(wrapper.emitted('submit')).toBeFalsy();
  });
});


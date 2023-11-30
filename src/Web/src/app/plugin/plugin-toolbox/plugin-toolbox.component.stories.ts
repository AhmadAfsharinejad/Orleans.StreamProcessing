import type { Meta, StoryObj } from '@storybook/angular';
import { argsToTemplate } from '@storybook/angular';
import {PluginToolboxComponent} from "./plugin-toolbox.component";

const meta: Meta<PluginToolboxComponent> = {
  title: 'PluginToolbox',
  component: PluginToolboxComponent,
  excludeStories: /.*Data$/,
  // tags: ['autodocs'],
  render: (args: PluginToolboxComponent) => ({
    props: {
      ...args
    },
    template: `<app-plugin-toolbox ${argsToTemplate(args)}></app-plugin-toolbox>`,
  }),
};

export default meta;
type Story = StoryObj<PluginToolboxComponent>;

export const Default: Story = {
  args: {
    plugin: {
      displayName: 'Sql Executor',
      pluginTypeId: {
        value : 'SqlExecutor'
      },
      iconPath: '/img/plugin-icons/sql-executor.png',
    },
  },
};

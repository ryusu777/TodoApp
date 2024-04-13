import type { Config } from 'tailwindcss';
import defaultTheme from 'tailwindcss/defaultTheme';
import { iconsPlugin, getIconCollections } from '@egoist/tailwindcss-icons';

export default <Partial<Config>> {
  plugins: [
    iconsPlugin({
      collections: getIconCollections(['heroicons'])
    })
  ],
  theme: {
    extend: {
      fontFamily: {
        'sans': ['"San Fransisco"', ...defaultTheme.fontFamily.sans],
      }
    }
  }
}

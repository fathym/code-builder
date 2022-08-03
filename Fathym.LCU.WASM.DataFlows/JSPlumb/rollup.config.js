import typescript from 'rollup-plugin-typescript2';
import pkg from './package.json';

const input = 'src/index.ts';

const typescriptPluginArgs = {
  typescript: require('typescript'),
};

export default {
  input,
  output: [
    {
      file: pkg.module,
      format: 'esm',
      sourcemap: false,
    },
  ],
  plugins: [
    typescript({
      ...typescriptPluginArgs,
      tsconfig: './tsconfig.json',
    }),
  ],
};

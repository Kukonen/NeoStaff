import ReactDOM from 'react-dom/client';
import { RouterProvider } from 'react-router-dom';
import { routing } from './router/router';

import './style/theme.css';
import './style/index.css';

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);

root.render(
    <RouterProvider router={routing} />
);


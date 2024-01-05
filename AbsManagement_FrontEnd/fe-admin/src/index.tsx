import { Provider } from 'react-redux';
import { PersistGate } from 'redux-persist/integration/react';
import ReactDOM from 'react-dom/client';
import reportWebVitals from './reportWebVitals';
import { RouterProvider } from 'react-router-dom'
import { PageLoading } from '@ant-design/pro-components';
import "./App.css"
import "./styles.scss"
import "@mapbox/mapbox-gl-geocoder/dist/mapbox-gl-geocoder.css";
import "mapbox-gl/dist/mapbox-gl.css";
import router from './routes/route-app';
import { ConfigProvider } from 'antd';
import en_US from 'antd/locale/en_US';

import store, { persistor } from './store/index';

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);

root.render(
  <Provider store={store}>
    <PersistGate loading={null} persistor={persistor}>
      <ConfigProvider locale={en_US}>
        <RouterProvider router={router} fallbackElement={<PageLoading />} />
      </ConfigProvider>
    </PersistGate>
  </Provider>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();

import React from 'react';
import ReactDOM from 'react-dom/client';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import './index.css';
import reportWebVitals from './reportWebVitals';

import App from "./components/App/App.tsx";
import './components/globals.ts';

import LoginPage from './components/pages/LoginPage/LoginPage.tsx';
import ClientsPage from './components/pages/ClientsPage/ClientsPage.tsx';
import ManagersPage from './components/pages/ManagersPage/ManagersPage.tsx';

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <Router>
      <App>
        <Routes>
          <Route path="/" element={<></>} />
          <Route path="/Login" element={<LoginPage></LoginPage>} />
          <Route path="/Clients" element={<ClientsPage></ClientsPage>} />
          <Route path="/Managers" element={<ManagersPage></ManagersPage>} />
        </Routes>
      </App>
    </Router>
  </React.StrictMode>
);

reportWebVitals();

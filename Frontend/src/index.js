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
import ProductsPage from './components/pages/ProductsPage/ProductsPage.tsx';
import DealsPage from './components/pages/DealsPage/DealsPage.tsx';
import AdminsPage from './components/pages/AdminsPage/AdminsPage.tsx';

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
          <Route path="/Products" element={<ProductsPage></ProductsPage>} />
          <Route path="/Deals" element={<DealsPage></DealsPage>} />
          <Route path="/Admins" element={<AdminsPage></AdminsPage>} />
        </Routes>
      </App>
    </Router>
  </React.StrictMode>
);

reportWebVitals();

import React, { FC, useState, useEffect } from 'react';
import {
   ClientsPageWrapper,
   ClientsPageContainer,
   ButtonsContainer,
   ClientsHeaderContainer,
   ClientsHeader
} from './ClientsPage.styled.ts';
import axios from 'axios';

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faCheck, faXmark, faArrowsRotate, faPlus } from '@fortawesome/free-solid-svg-icons'

import { Table, Button, Spinner } from 'react-bootstrap';

import ClientModal from '../../modals/ClientModal/ClientModal.tsx';
import AddClientModal from '../../modals/AddClientModal/AddClientModal.tsx';

import { apiUrl } from '../../config.ts';

interface ClientsPageProps { }

type Client = {
   id: string;
   name: string;
   lastName: string;
   email: string;
   phoneNumber: string;
   address: string;
   companyName: string;
   notes?: string;
   createdAt: string;
   updatedAt: string;
   isActive: boolean;
};

const ClientsPage: FC<ClientsPageProps> = () => {
   const [clients, setClients] = useState<Client[]>([]);

   const [loading, setLoading] = useState(false);

   const [selectedClient, setSelectedClient] = useState(null);
   const [showEditModal, setShowEditModal] = useState(false);
   const [showAddModal, setShowAddModal] = useState(false);

   const fetchClients = async () => {
      setLoading(true);

      try {
         const response = await axios.get<Client[]>(`${apiUrl}/client`);

         setClients(response.data);
      } catch (error) {
         console.error('Error fetching clients:', error);
      } finally {
         setLoading(false);
      }
   };

   useEffect(() => {
      fetchClients();
   }, []);

   const updateClientList = () => {
      fetchClients();
   };

   const handleRowClick = (client) => {
      setSelectedClient(client);
      setShowEditModal(true);
   };

   const handleCloseEditModal = () => {
      setSelectedClient(null);
      setShowEditModal(false);
   };

   const handleCloseAddModal = () => {
      setShowAddModal(false);
   };

   const handleAddModal = () => {
      setShowAddModal(true);
   };

   return (
      <ClientsPageWrapper>
         <ClientsPageContainer>
            <ClientsHeaderContainer>
               <ClientsHeader>Clients management</ClientsHeader>
               <ButtonsContainer>
                  <Button variant="success" onClick={handleAddModal} style={{ marginRight: "12px" }}><FontAwesomeIcon icon={faPlus} /></Button>
                  <Button variant="dark" onClick={fetchClients}><FontAwesomeIcon icon={faArrowsRotate} /></Button>
               </ButtonsContainer>
            </ClientsHeaderContainer>
            {loading ? (
               <div className="d-flex justify-content-center align-items-center" style={{ height: "400px" }}>
                  <Spinner animation="border" style={{ color: "white" }} />
               </div>
            ) : (
               <Table
                  bordered hover responsive
                  variant="dark"
                  style={{
                     borderColor: 'rgb(23, 25, 27)',
                     width: "1120px"
                  }}
               >
                  <thead>
                     <tr>
                        <th>Name</th>
                        <th>Last Name</th>
                        <th>Email</th>
                        <th>Phone Number</th>
                        <th>Address</th>
                        <th>Created At</th>
                        <th style={{ textAlign: "center" }}>Status</th>
                     </tr>
                  </thead>
                  <tbody>
                     {clients.map((client) => (
                        <tr
                           key={client.id}
                           onClick={() => handleRowClick(client)}
                           style={{
                              cursor: 'pointer',
                           }}
                        >
                           <td>{client.name}</td>
                           <td>{client.lastName}</td>
                           <td>{client.email}</td>
                           <td>{client.phoneNumber}</td>
                           <td>{client.address}</td>
                           <td>{new Date(client.createdAt).toLocaleString()}</td>
                           <td style={{ textAlign: "center" }}>
                              {client.isActive ? (
                                 <FontAwesomeIcon icon={faCheck} />
                              ) : (
                                 <FontAwesomeIcon icon={faXmark} />
                              )}
                           </td>
                        </tr>
                     ))}
                  </tbody>
               </Table>
            )}
            <ClientModal
               show={showEditModal}
               handleClose={handleCloseEditModal}
               client={selectedClient}
               onClientUpdated={updateClientList}
            />
            <AddClientModal
               show={showAddModal}
               handleClose={handleCloseAddModal}
               onClientUpdated={updateClientList}
            />
         </ClientsPageContainer>
      </ClientsPageWrapper >
   );
};

export default ClientsPage;

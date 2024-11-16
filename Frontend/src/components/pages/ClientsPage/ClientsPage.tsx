import React, { FC, useState, useEffect } from 'react';
import {
   ClientsPageWrapper,
   ClientsPageContainer
} from './ClientsPage.styled.ts';
import axios from 'axios';

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faCheck, faXmark } from '@fortawesome/free-solid-svg-icons'

import { Table } from 'react-bootstrap';

import ClientModal from '../../modals/ClientModal/ClientModal.tsx';

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

   const [selectedClient, setSelectedClient] = useState(null);
   const [showModal, setShowModal] = useState(false);

   useEffect(() => {
      const fetchClients = async () => {
         const response = await axios.get<Client[]>(`${apiUrl}/client`);

         setClients(response.data);
      };

      fetchClients();
   }, []);

   const handleRowClick = (client) => {
      setSelectedClient(client);
      setShowModal(true);
   };

   const handleCloseModal = () => {
      setSelectedClient(null);
      setShowModal(false);
   };

   return (
      <ClientsPageWrapper>
         <ClientsPageContainer>
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
            <ClientModal
               show={showModal}
               handleClose={handleCloseModal}
               client={selectedClient}
            />
         </ClientsPageContainer>
      </ClientsPageWrapper >
   );
};

export default ClientsPage;

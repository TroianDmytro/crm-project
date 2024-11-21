import React, { FC, useState, useEffect } from 'react';
import { 
   ManagersPageWrapper,
   ManagersPageContainer,
   ButtonsContainer,
   ManagersHeaderContainer,
   ManagersHeader
} from './ManagersPage.styled.ts';

import axios from 'axios';

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faCheck, faXmark, faArrowsRotate, faPlus } from '@fortawesome/free-solid-svg-icons'

import { Table, Button, Spinner } from 'react-bootstrap';

import ManagerModal from '../../modals/ManagerModal/ManagerModal.tsx';
import AddManagerModal from '../../modals/AddManagerModal/AddManagerModal.tsx';

import { apiUrl } from '../../config.ts';

interface ManagersPageProps { }

type Manager = {
   id: string;
   name: string;
   lastName: string;
   phoneNumber: string;
   patronymic?: string;
   userName: string;
   address?: string;
   dateOfBirth: string;
   hireDate?: string;
   position?: string;
   department?: string;
   email: string;
   password: string;
};

const ManagersPage: FC<ManagersPageProps> = () => {
   const [managers, setManagers] = useState<Manager[]>([]);

   const [loading, setLoading] = useState(false);

   const [selectedManager, setSelectedManager] = useState(null);
   const [showEditModal, setShowEditModal] = useState(false);
   const [showAddModal, setShowAddModal] = useState(false);

   const fetchManagers = async () => {
      setLoading(true);

      try {
         const response = await axios.get<Manager[]>(`${apiUrl}/manager`); // Неіснуючий шлях. Змінити

         setManagers(response.data);
      } catch (error) {
         console.error('Error fetching managers:', error);
      } finally {
         setLoading(false);
      }
   };

   useEffect(() => {
      fetchManagers();
   }, []);

   const updateManagerList = () => {
      fetchManagers();
   };

   const handleRowClick = (manager) => {
      setSelectedManager(manager);
      setShowEditModal(true);
   };

   const handleCloseEditModal = () => {
      setSelectedManager(null);
      setShowEditModal(false);
   };

   const handleCloseAddModal = () => {
      setShowAddModal(false);
   };

   const handleAddModal = () => {
      setShowAddModal(true);
   };

   return (
      <ManagersPageWrapper>
         <ManagersPageContainer>
            <ManagersHeaderContainer>
               <ManagersHeader>Managers management</ManagersHeader>
               <ButtonsContainer>
                  <Button variant="success" onClick={handleAddModal} style={{ marginRight: "12px" }}><FontAwesomeIcon icon={faPlus} /></Button>
                  <Button variant="dark" onClick={fetchManagers}><FontAwesomeIcon icon={faArrowsRotate} /></Button>
               </ButtonsContainer>
            </ManagersHeaderContainer>
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
                        <th>Username</th>
                        <th>Email</th>
                        <th>Phone Number</th>
                        <th>Address</th>
                        <th>Position</th>
                     </tr>
                  </thead>
                  <tbody>
                     {managers.map((manager) => (
                        <tr
                           key={manager.id}
                           onClick={() => handleRowClick(manager)}
                           style={{
                              cursor: 'pointer',
                           }}
                        >
                           <td>{manager.name}</td>
                           <td>{manager.lastName}</td>
                           <td>{manager.userName}</td>
                           <td>{manager.email}</td>
                           <td>{manager.phoneNumber}</td>
                           <td>{manager.address}</td>
                           <td>{manager.position}</td>
                        </tr>
                     ))}
                  </tbody>
               </Table>
            )}
            <ManagerModal
               show={showEditModal}
               handleClose={handleCloseEditModal}
               manager={selectedManager}
               onManagerUpdated={updateManagerList}
            />
            <AddManagerModal
               show={showAddModal}
               handleClose={handleCloseAddModal}
               onManagerUpdated={updateManagerList}
            />
         </ManagersPageContainer>
      </ManagersPageWrapper >
   );
};

export default ManagersPage;

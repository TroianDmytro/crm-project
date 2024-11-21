import React, { FC, useState, useEffect } from 'react';
import {
   ManagersPageWrapper,
   ManagersPageContainer,
   ManagersHeaderContainer,
   HeaderText
} from './ManagersPage.styled.ts';

import axios from 'axios';

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faPlus } from '@fortawesome/free-solid-svg-icons'

import { Form, Button, Spinner } from 'react-bootstrap';

import { apiUrl } from '../../config.ts';

interface ManagersPageProps { }

type FormData = {
   name: string,
   lastName: string,
   patronymic: string,
   userName: string,
   address: string,
   dateOfBirth: string,
   hireDate: string,
   position: string,
   department: string,
   email: string,
   password: string
}

const ManagersPage: FC<ManagersPageProps> = () => {
   const [loading, setLoading] = useState(false);
   const [formData, setFormData] = useState<FormData>({
      name: "",
      lastName: "",
      patronymic: "",
      userName: "",
      address: "",
      dateOfBirth: "",
      hireDate: "",
      position: "",
      department: "",
      email: "",
      password: ""
   });

   const handleClear = async () => {
      setFormData({
         name: "",
         lastName: "",
         patronymic: "",
         userName: "",
         address: "",
         dateOfBirth: "",
         hireDate: "",
         position: "",
         department: "",
         email: "",
         password: ""
      });
   }

   const handleInputChange = (e) => {
      const { name, value } = e.target;
      setFormData((prev) => ({
         ...prev,
         [name]: value,
      }));
   };

   const handleConfirm = async () => {
      setLoading(true);

      try {
         const transformedData = {
            ...formData,
            dateOfBirth: new Date(formData.dateOfBirth).toISOString(),
            hireDate: formData.hireDate ? new Date(formData.hireDate).toISOString() : null,
         };

         const response = await axios.post(`${apiUrl}/auth/register_manager`, transformedData);

         console.log("Manager added successfully:", response.data);

         handleClear();
      } catch (error) {
         console.error("Error adding manager:", error);
      } finally {
         setLoading(false);
      }
   };

   return (
      <ManagersPageWrapper>
         <ManagersPageContainer>
            <ManagersHeaderContainer>
               <Form>
                  <HeaderText>Add manager</HeaderText>
                  <Form.Group className="mb-3 d-flex">
                     <Form.Label style={{ color: "white" }} className="me-2">Name:</Form.Label>
                     <Form.Control
                        type="text"
                        name="name"
                        value={formData.name}
                        onChange={handleInputChange}
                        placeholder="Enter name"
                     />
                  </Form.Group>
                  <Form.Group className="mb-3 d-flex">
                     <Form.Label style={{ color: "white" }} className="me-2">Last name:</Form.Label>
                     <Form.Control
                        type="text"
                        name="lastName"
                        value={formData.lastName}
                        onChange={handleInputChange}
                        placeholder="Enter last name"
                     />
                  </Form.Group>
                  <Form.Group className="mb-3 d-flex">
                     <Form.Label style={{ color: "white" }} className="me-2">Patronymic:</Form.Label>
                     <Form.Control
                        type="text"
                        name="patronymic"
                        value={formData.patronymic}
                        onChange={handleInputChange}
                        placeholder="Enter patronymic"
                     />
                  </Form.Group>
                  <Form.Group className="mb-3 d-flex">
                     <Form.Label style={{ color: "white" }} className="me-2">Username:</Form.Label>
                     <Form.Control
                        type="tel"
                        name="userName"
                        value={formData.userName}
                        onChange={handleInputChange}
                        placeholder="Enter username"
                     />
                  </Form.Group>
                  <Form.Group className="mb-3 d-flex">
                     <Form.Label style={{ color: "white" }} className="me-2">Address:</Form.Label>
                     <Form.Control
                        type="text"
                        name="address"
                        value={formData.address}
                        onChange={handleInputChange}
                        placeholder="Enter address"
                     />
                  </Form.Group>
                  <Form.Group className="mb-3 d-flex">
                     <Form.Label style={{ color: "white" }} className="me-2">Date of birth:</Form.Label>
                     <Form.Control
                        type="date"
                        name="dateOfBirth"
                        value={formData.dateOfBirth}
                        onChange={handleInputChange}
                        placeholder="Enter date of birth"
                     />
                  </Form.Group>
                  <Form.Group className="mb-3 d-flex">
                     <Form.Label style={{ color: "white" }} className="me-2">Hire date:</Form.Label>
                     <Form.Control
                        type="date"
                        name="hireDate"
                        value={formData.hireDate}
                        onChange={handleInputChange}
                        placeholder="Enter hire date"
                     />
                  </Form.Group>
                  <Form.Group className="mb-3 d-flex">
                     <Form.Label style={{ color: "white" }} className="me-2">Position:</Form.Label>
                     <Form.Control
                        type="text"
                        name="position"
                        value={formData.position}
                        onChange={handleInputChange}
                        placeholder="Enter position"
                     />
                  </Form.Group>
                  <Form.Group className="mb-3 d-flex">
                     <Form.Label style={{ color: "white" }} className="me-2">Department:</Form.Label>
                     <Form.Control
                        type="text"
                        name="department"
                        value={formData.department}
                        onChange={handleInputChange}
                        placeholder="Enter department"
                     />
                  </Form.Group>
                  <Form.Group className="mb-3 d-flex">
                     <Form.Label style={{ color: "white" }} className="me-2">Email:</Form.Label>
                     <Form.Control
                        type="text"
                        name="email"
                        value={formData.email}
                        onChange={handleInputChange}
                        placeholder="Enter email"
                     />
                  </Form.Group>
                  <Form.Group className="mb-3 d-flex">
                     <Form.Label style={{ color: "white" }} className="me-2">Password:</Form.Label>
                     <Form.Control
                        type="text"
                        name="password"
                        value={formData.password}
                        onChange={handleInputChange}
                        placeholder="Enter password"
                     />
                  </Form.Group>
                  <Button style={{ width: "100%" }} variant="success" onClick={handleConfirm}>
                     {loading ? <><Spinner animation="border" style={{ width: '18px', height: '18px' }} /> Loading...</> : <><FontAwesomeIcon style={{ marginRight: "4px" }} icon={faPlus} /> Add manager</>}
                  </Button>
               </Form>
            </ManagersHeaderContainer>
         </ManagersPageContainer>
      </ManagersPageWrapper >
   );
};

export default ManagersPage;

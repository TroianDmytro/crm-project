import React, { useState, useEffect } from 'react';
import { Modal, Button, Form, Spinner } from 'react-bootstrap';

import axios from 'axios';

import "./../Modal.css";

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faCheck, faXmark, faPenToSquare, faTrash } from '@fortawesome/free-solid-svg-icons'

import { apiUrl } from '../../config.ts';

type FormData = {
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

const ManagerModal = ({ show, handleClose, manager, onManagerUpdated }) => {
   const [loading, setLoading] = useState(false);

   const [currentState, setCurrentState] = useState("default");
   const [status, setStatus] = useState(Boolean);

   const [formData, setFormData] = useState<FormData>({
      name: "",
      lastName: "",
      phoneNumber: "",
      patronymic: "",
      userName: "",
      address: "",
      dateOfBirth: "",
      hireDate: "",
      position: "",
      department: "",
      email: "",
      password: "",
   });

   useEffect(() => {
      if (show) {
         setCurrentState("default");
      }
   }, [show]);

   if (!manager) return null;

   const handleEdit = () => {
      setStatus(manager.isActive);
      setFormData({
         name: manager.name,
         lastName: manager.lastName,
         phoneNumber: manager.phoneNumber,
         patronymic: manager.patronymic,
         userName: manager.userName,
         address: manager.address,
         dateOfBirth: manager.dateOfBirth,
         hireDate: manager.hireDate,
         position: manager.position,
         department: manager.department,
         email: manager.email,
         password: manager.password
      });
      setCurrentState("edit");
   };

   const handleDelete = () => {
      setCurrentState("delete");
   };

   const handleCancel = () => {
      setCurrentState("default");
   };

   const handleConfirm = async () => {
      setLoading(true);
      if (currentState === "edit") {
         try {
            await axios.put(`${apiUrl}/manager/edit/${manager.id}`, {
               ...formData,
               isActive: status,
            });
            onManagerUpdated();

            handleClose();
         } catch (error) {
            console.error("Error updating manager:", error);
         } finally {
            setLoading(false);
         }
      } else if (currentState === "delete") {
         try {
            await axios.delete(`${apiUrl}/manager/remove/${manager.id}`);
            onManagerUpdated();

            handleClose();
         } catch (error) {
            console.error("Error deleting manager:", error);
         } finally {
            setLoading(false);
         }
      }
   };

   const handleStatusChange = () => {
      setStatus(!status);
   };

   const handleInputChange = (e) => {
      const { name, value } = e.target;
      setFormData((prev) => ({
         ...prev,
         [name]: value,
      }));
   };

   return (
      <Modal
         show={show} onHide={handleClose} centered size="lg" backdrop="static"
         style={{
            backgroundColor: "rgba(33, 37, 41, 0.525)"
         }}
      >
         <Modal.Header
            closeButton
            className='Dark'
            style={{
               borderBottom: "2px rgb(23, 25, 27) solid",
               justifyContent: "space-between"
            }}
         >
            <Modal.Title>Manager details</Modal.Title>
            <FontAwesomeIcon
               icon={faXmark}
               onClick={handleClose}
               style={{
                  cursor: "pointer",
                  fontSize: "160%"
               }}
            />
         </Modal.Header>
         {currentState === "edit" ? (
            <Modal.Body className='Dark'>
               <Form>
                  <Form.Group className="mb-3 d-flex">
                     <Form.Label className="me-2">Name:</Form.Label>
                     <Form.Control
                        type="text"
                        name="name"
                        value={formData.name}
                        onChange={handleInputChange}
                        placeholder="Enter name"
                     />
                  </Form.Group>
                  <Form.Group className="mb-3 d-flex">
                     <Form.Label className="me-2">Last name:</Form.Label>
                     <Form.Control
                        type="text"
                        name="lastName"
                        value={formData.lastName}
                        onChange={handleInputChange}
                        placeholder="Enter last name"
                     />
                  </Form.Group>
                  <Form.Group className="mb-3 d-flex">
                     <Form.Label className="me-2">Patronymic:</Form.Label>
                     <Form.Control
                        type="text"
                        name="patronymic"
                        value={formData.patronymic}
                        onChange={handleInputChange}
                        placeholder="Enter patronymic"
                     />
                  </Form.Group>
                  <Form.Group className="mb-3 d-flex">
                     <Form.Label className="me-2">Username:</Form.Label>
                     <Form.Control
                        type="text"
                        name="userName"
                        value={formData.userName}
                        onChange={handleInputChange}
                        placeholder="Enter username"
                     />
                  </Form.Group>
                  <Form.Group className="mb-3 d-flex">
                     <Form.Label className="me-2">Address:</Form.Label>
                     <Form.Control
                        type="text"
                        name="address"
                        value={formData.address}
                        onChange={handleInputChange}
                        placeholder="Enter address"
                     />
                  </Form.Group>
                  <Form.Group className="mb-3 d-flex">
                     <Form.Label className="me-2">Date of birth:</Form.Label>
                     <Form.Control
                        type="text"
                        name="dateOfBirth"
                        value={formData.dateOfBirth}
                        onChange={handleInputChange}
                        placeholder="Enter date of birth"
                     />
                  </Form.Group>
                  <Form.Group className="mb-3 d-flex">
                     <Form.Label className="me-2">Hire date:</Form.Label>
                     <Form.Control
                        type="text"
                        name="hireDate"
                        value={formData.hireDate}
                        onChange={handleInputChange}
                        placeholder="Enter hire date"
                     />
                  </Form.Group>
                  <Form.Group className="mb-3 d-flex">
                     <Form.Label className="me-2">Position:</Form.Label>
                     <Form.Control
                        type="text"
                        name="position"
                        value={formData.position}
                        onChange={handleInputChange}
                        placeholder="Enter position"
                     />
                  </Form.Group>
                  <Form.Group className="mb-3 d-flex">
                     <Form.Label className="me-2">Department:</Form.Label>
                     <Form.Control
                        type="text"
                        name="department"
                        value={formData.department}
                        onChange={handleInputChange}
                        placeholder="Enter department"
                     />
                  </Form.Group>
                  <Form.Group className="mb-3 d-flex">
                     <Form.Label className="me-2">Email:</Form.Label>
                     <Form.Control
                        type="text"
                        name="email"
                        value={formData.email}
                        onChange={handleInputChange}
                        placeholder="Enter email"
                     />
                  </Form.Group>
                  <Form.Group className="mb-3 d-flex">
                     <Form.Label className="me-2">Password:</Form.Label>
                     <Form.Control
                        type="text"
                        name="password"
                        value={formData.password}
                        onChange={handleInputChange}
                        placeholder="Enter password"
                     />
                  </Form.Group>
               </Form>
            </Modal.Body>
         ) : (
            <Modal.Body className='Dark'>
               <h5 style={{ marginBottom: "18px" }}>Name: {manager?.name} {manager?.lastName}</h5>
               <p><strong>Patronymic:</strong> {manager?.patronymic}</p>
               <p><strong>Username:</strong> {manager.userName}</p>
               <p><strong>Address:</strong> {manager?.address}</p>
               <p><strong>Date of birth:</strong> {new Date(manager?.dateOfBirth).toLocaleString()}</p>
               <p><strong>Hire date:</strong> {new Date(manager?.hireDate).toLocaleString()}</p>
               <p><strong>Position:</strong> {manager?.position}</p>
               <p><strong>Department:</strong> {manager?.department}</p>
               <p><strong>Email:</strong> {manager.email}</p>
               <p><strong>Password:</strong> {manager.password}</p>
            </Modal.Body>
         )}
         <Modal.Footer
            className='Dark'
            style={{
               borderTop: "2px rgb(23, 25, 27) solid",
               justifyContent: "space-between"
            }}
         >
            <span>{currentState === "delete" ? "Deleting..." : currentState === "edit" ? "Editing..." : ""}</span>
            {currentState == "default" ? (
               <div>
                  <Button variant="dark" onClick={handleEdit} style={{ marginRight: "8px" }}><FontAwesomeIcon icon={faPenToSquare} /> Edit</Button>
                  <Button variant="danger" onClick={handleDelete}><FontAwesomeIcon icon={faTrash} /> Delete</Button>
               </div>
            ) : (
               <div>
                  <Button
                     variant={currentState === "delete" ? "danger" : "success"}
                     style={{ marginRight: "8px" }}
                     onClick={handleConfirm}
                  >
                     {loading ? <Spinner animation="border" /> : <><FontAwesomeIcon icon={faCheck} /> Confirm</>}
                  </Button>
                  <Button variant="dark" onClick={handleCancel}>
                     <FontAwesomeIcon icon={faXmark} />
                  </Button>
               </div>
            )}
         </Modal.Footer>
      </Modal>
   );
};

export default ManagerModal;
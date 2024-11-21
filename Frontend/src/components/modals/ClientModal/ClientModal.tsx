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
   email: string;
   phoneNumber: string;
   address: string;
   companyName: string;
   notes: string;
};

const ClientModal = ({ show, handleClose, client, onClientUpdated }) => {
   const [loading, setLoading] = useState(false);

   const [currentState, setCurrentState] = useState("default");
   const [status, setStatus] = useState(Boolean);

   const [formData, setFormData] = useState<FormData>({
      name: "",
      lastName: "",
      email: "",
      phoneNumber: "",
      address: "",
      companyName: "",
      notes: ""
   });

   useEffect(() => {
      if (show) {
         setCurrentState("default");
      }
   }, [show]);

   if (!client) return null;

   const handleEdit = () => {
      setStatus(client.isActive);
      setFormData({
         name: client.name,
         lastName: client.lastName,
         email: client.email,
         phoneNumber: client.phoneNumber,
         address: client.address,
         companyName: client.companyName,
         notes: client.notes
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
            await axios.put(`${apiUrl}/client/edit/${client.id}`, {
               ...formData,
               isActive: status,
            });
            onClientUpdated();

            handleClose();
         } catch (error) {
            console.error("Error updating client:", error);
         } finally {
            setLoading(false);
         }
      } else if (currentState === "delete") {
         try {
            await axios.delete(`${apiUrl}/client/remove/${client.id}`);
            onClientUpdated();

            handleClose();
         } catch (error) {
            console.error("Error deleting client:", error);
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
            <Modal.Title>Client details</Modal.Title>
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
                     <Form.Label className="me-2">Email:</Form.Label>
                     <Form.Control
                        type="email"
                        name="email"
                        value={formData.email}
                        onChange={handleInputChange}
                        placeholder="Enter email"
                     />
                  </Form.Group>
                  <Form.Group className="mb-3 d-flex">
                     <Form.Label className="me-2">Phone number:</Form.Label>
                     <Form.Control
                        type="tel"
                        name="phoneNumber"
                        value={formData.phoneNumber}
                        onChange={handleInputChange}
                        placeholder="Enter phone number"
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
                     <Form.Label className="me-2">Company name:</Form.Label>
                     <Form.Control
                        type="text"
                        name="companyName"
                        value={formData.companyName}
                        onChange={handleInputChange}
                        placeholder="Enter company name"
                     />
                  </Form.Group>
                  <Form.Group className="mb-3 d-flex">
                     <Form.Label className="me-2">Notes:</Form.Label>
                     <Form.Control
                        as="textarea"
                        rows={3}
                        name="notes"
                        value={formData.notes}
                        onChange={handleInputChange}
                        placeholder="Enter notes"
                     />
                  </Form.Group>
                  <Form.Group className="mb-3 d-flex">
                     <Form.Label className="me-2">Status:</Form.Label>
                     <Button
                        variant={status ? "success" : "danger"}
                        onClick={handleStatusChange}
                     >
                        {status ? <FontAwesomeIcon icon={faCheck} /> : <FontAwesomeIcon icon={faXmark} />}
                     </Button>
                  </Form.Group>
               </Form>
            </Modal.Body>
         ) : (
            <Modal.Body className='Dark'>
               <h5 style={{ marginBottom: "18px" }}>Name: {client?.name} {client?.lastName}</h5>
               <p><strong>Email:</strong> {client?.email}</p>
               <p><strong>Phone:</strong> {client?.phoneNumber}</p>
               <p><strong>Address:</strong> {client?.address}</p>
               <p><strong>Company:</strong> {client?.companyName}</p>
               <p><strong>Notes:</strong> {client?.notes ? client?.notes : <FontAwesomeIcon icon={faXmark} />}</p>
               <p><strong>Created At:</strong> {new Date(client?.createdAt).toLocaleString()}</p>
               <p><strong>Updated At:</strong> {client?.updatedAt ? new Date(client?.updatedAt).toLocaleString() : 'N/A'}</p>
               <p style={{ margin: "0" }}><strong style={{ marginRight: "8px" }}>Status:</strong>
                  {client.isActive ? (
                     <FontAwesomeIcon icon={faCheck} />
                  ) : (
                     <FontAwesomeIcon icon={faXmark} />
                  )}
               </p>
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
                     {loading ? <Spinner animation="border" style={{ width: '18px', height: '18px' }} /> : <><FontAwesomeIcon icon={faCheck} /> Confirm</>}
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

export default ClientModal;
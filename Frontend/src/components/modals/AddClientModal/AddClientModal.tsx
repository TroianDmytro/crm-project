import React, { useState, useEffect } from 'react';
import { Modal, Button, Form, Spinner } from 'react-bootstrap';

import axios from 'axios';

import "./../Modal.css";

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faCheck, faXmark, faPlus, faEraser } from '@fortawesome/free-solid-svg-icons'

import { apiUrl } from '../../config.ts';

type FormData = {
   name: string;
   lastName: string;
   email: string;
   phoneNumber: string;
   address: string;
   companyName: string;
   notes: string;
   isActive: boolean;
};

const AddClientModal = ({ show, handleClose, onClientUpdated }) => {
   const [loading, setLoading] = useState(false);

   const [status, setStatus] = useState(false);
   const [formData, setFormData] = useState<FormData>({
      name: "",
      lastName: "",
      email: "",
      phoneNumber: "",
      address: "",
      companyName: "",
      notes: "",
      isActive: false
   });

   useEffect(() => {
      if (!show) {
         handleClear();
      }
   }, [show]);

   const handleClear = () => {
      setStatus(false);
      setFormData({
         name: "",
         lastName: "",
         email: "",
         phoneNumber: "",
         address: "",
         companyName: "",
         notes: "",
         isActive: false,
      });
   };

   const handleConfirm = async () => {
      setLoading(true);

      try {
         const response = await axios.post(`${apiUrl}/client/add/`, formData);
         console.log("Client added successfully:", response.data);

         if (onClientUpdated) {
            onClientUpdated(response.data);
         }

         handleClear();
         handleClose();
      } catch (error) {
         console.error("Error adding client:", error);
      } finally {
         setLoading(false);
      }
   };

   const handleStatusChange = () => {
      setStatus(!status);
      setFormData((prev) => ({ ...prev, isActive: !status }));
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
            <Modal.Title>Add client</Modal.Title>
            <FontAwesomeIcon
               icon={faXmark}
               onClick={handleClose}
               style={{
                  cursor: "pointer",
                  fontSize: "160%"
               }}
            />
         </Modal.Header>
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
         <Modal.Footer
            className='Dark'
            style={{
               borderTop: "2px rgb(23, 25, 27) solid"
            }}
         >
            <Button
               variant="success"
               style={{ marginRight: "8px" }}
               onClick={handleConfirm}
            >
               {loading ? <Spinner animation="border" style={{ width: '18px', height: '18px' }} /> : <><FontAwesomeIcon icon={faPlus} /> Add</>}
            </Button>
            <Button variant="dark" onClick={handleClear}><FontAwesomeIcon icon={faEraser} /></Button>
         </Modal.Footer>
      </Modal>
   );
};

export default AddClientModal;
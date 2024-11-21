import React, { useState, useEffect } from 'react';
import { Modal, Button, Form, Spinner } from 'react-bootstrap';

import axios from 'axios';

import "./../Modal.css";

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faCheck, faXmark, faPlus, faEraser, faClockRotateLeft } from '@fortawesome/free-solid-svg-icons'

import { apiUrl } from '../../config.ts';

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

type FormData = {
   title: string;
   amount: number;
   expectedCloseDate: string;
   status: string;
   createdAt: string;
   client: Client | null | string;
   clientName: string;
   clientLastName: string;
};

const AddDealModal = ({ show, handleClose, onDealUpdated }) => {
   const [loading, setLoading] = useState(false);

   const [status, setStatus] = useState("Done");
   const [formData, setFormData] = useState<FormData>({
      title: "",
      amount: 0,
      expectedCloseDate: "",
      status: "Done",
      createdAt: "",
      client: null,
      clientName: "",
      clientLastName: ""
   });

   useEffect(() => {
      if (!show) {
         handleClear();
      }
   }, [show]);

   const handleClear = () => {
      setStatus("Done");
      setFormData({
         title: "",
         amount: 0,
         expectedCloseDate: "",
         status: "Done",
         createdAt: "",
         client: null,
         clientName: "",
         clientLastName: ""
      });
   };

   const handleConfirm = async () => {
      setLoading(true);

      console.log(formData);

      try {
         const response = await axios.post(`${apiUrl}/deal/create/`, formData);
         console.log("Deal added successfully:", response.data);

         if (onDealUpdated) {
            onDealUpdated(response.data);
         }

         handleClear();
         handleClose();
      } catch (error) {
         console.error("Error adding deal:", error);
      } finally {
         setLoading(false);
      }
   };

   const handleStatusChange = () => {
      if (status === "Done") {
         setStatus("New");
      }
      else if (status === "New") {
         setStatus("In process");
      }
      else {
         setStatus("Done");
      }
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
            <Modal.Title>Add deal</Modal.Title>
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
                     <Form.Label className="me-2">Title:</Form.Label>
                     <Form.Control
                        type="text"
                        name="title"
                        value={formData.title}
                        onChange={handleInputChange}
                        placeholder="Enter title"
                     />
                  </Form.Group>
                  <Form.Group className="mb-3 d-flex">
                     <Form.Label className="me-2">Amount:</Form.Label>
                     <Form.Control
                        type="text"
                        name="amount"
                        value={formData.amount}
                        onChange={handleInputChange}
                        placeholder="Enter amount"
                     />
                  </Form.Group>
                  <Form.Group className="mb-3 d-flex">
                     <Form.Label className="me-2">Expected close date:</Form.Label>
                     <Form.Control
                        type="text"
                        name="expectedCloseDate"
                        value={formData.expectedCloseDate}
                        onChange={handleInputChange}
                        placeholder="Enter expected close date"
                     />
                  </Form.Group>
                  <Form.Group className="mb-3 d-flex">
                     <Form.Label className="me-2">Status:</Form.Label>
                     <Button variant={status === "Done" ? "success" : status === "New" ? "danger" : "warning"} onClick={handleStatusChange}>
                        {status === "New" ? (
                           <FontAwesomeIcon style={{ marginRight: "4px" }} icon={faPlus} />
                        ) : status === "In process" ? (
                           <FontAwesomeIcon style={{ marginRight: "4px" }} icon={faClockRotateLeft} />
                        ) : (
                           <FontAwesomeIcon style={{ marginRight: "4px" }} icon={faCheck} />
                        )}
                        {status}
                     </Button>
                  </Form.Group>
                  <Form.Group className="mb-3 d-flex">
                     <Form.Label className="me-2">Created at:</Form.Label>
                     <Form.Control
                        type="text"
                        name="createdAt"
                        value={formData.createdAt}
                        onChange={handleInputChange}
                        placeholder="Created at"
                     />
                  </Form.Group>
                  <Form.Group className="mb-3 d-flex">
                     <Form.Label className="me-2">Client Name:</Form.Label>
                     <Form.Control
                        type="text"
                        name="clientName"
                        value={formData.clientName}
                        onChange={handleInputChange}
                        placeholder="Enter client name"
                     />
                  </Form.Group>
                  <Form.Group className="mb-3 d-flex">
                     <Form.Label className="me-2">Client Last Name:</Form.Label>
                     <Form.Control
                        type="text"
                        name="clientLastName"
                        value={formData.clientLastName}
                        onChange={handleInputChange}
                        placeholder="Enter client last name"
                     />
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

export default AddDealModal;
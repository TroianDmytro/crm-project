import React, { useState, useEffect } from 'react';
import { Modal, Button, Form, Spinner } from 'react-bootstrap';

import axios from 'axios';

import "./../Modal.css";

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faCheck, faXmark, faPenToSquare, faTrash, faClockRotateLeft, faPlus } from '@fortawesome/free-solid-svg-icons'

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

const DealModal = ({ show, handleClose, deal, onDealUpdated }) => {
   const [loading, setLoading] = useState(false);

   const [currentState, setCurrentState] = useState("default");
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
      if (show) {
         setCurrentState("default");
      }
   }, [show]);

   if (!deal) return null;

   const handleEdit = () => {
      setStatus(deal.isActive);
      setFormData({
         title: deal.title,
         amount: deal.amount,
         expectedCloseDate: deal.expectedCloseDate,
         status: deal.status,
         createdAt: deal.createdAt,
         client: deal.client,
         clientName: deal.client.name,
         clientLastName: deal.client.lastName
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
            await axios.put(`${apiUrl}/deal/edit/${deal.id}`, {
               ...formData,
               status: status,
            });
            onDealUpdated();

            handleClose();
         } catch (error) {
            console.error("Error updating deal:", error);
         } finally {
            setLoading(false);
         }
      } else if (currentState === "delete") {
         try {
            await axios.delete(`${apiUrl}/deal/remove/${deal.id}`);
            onDealUpdated();

            handleClose();
         } catch (error) {
            console.error("Error deleting deal:", error);
         } finally {
            setLoading(false);
         }
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
            <Modal.Title>Deal details</Modal.Title>
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
                     <Button variant={status === "In stock" ? "success" : status === "To order" ? "warning" : "danger"} onClick={handleStatusChange}>
                        {deal.status === "New" ? (
                           <FontAwesomeIcon style={{ marginRight: "4px" }} icon={faPlus} />
                        ) : deal.status === "In process" ? (
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
         ) : (
            <Modal.Body className='Dark'>
               <p><strong>Title:</strong> {deal?.title}</p>
               <p><strong>Amount:</strong> {deal?.amount}</p>
               <p><strong>Expected close date:</strong> {deal?.expectedCloseDate ? new Date(deal?.expectedCloseDate).toLocaleString() : 'N/A'}</p>
               <p><strong style={{ marginRight: "4px" }}>Status:</strong>
                  {deal.status === "New" ? (
                     <FontAwesomeIcon style={{ marginRight: "4px" }} icon={faPlus} />
                  ) : deal.status === "In process" ? (
                     <FontAwesomeIcon style={{ marginRight: "4px" }} icon={faClockRotateLeft} />
                  ) : (
                     <FontAwesomeIcon style={{ marginRight: "4px" }} icon={faCheck} />
                  )}
                  {deal.status}
               </p>
               <p><strong>Created at:</strong> {new Date(deal?.createdAt).toLocaleString()}</p>
               <p><strong>Title:</strong> {deal?.title}</p>
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

export default DealModal;
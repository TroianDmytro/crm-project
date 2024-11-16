import React from 'react';
import { Modal, Button } from 'react-bootstrap';

import "./ClientModal.css";

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faCheck, faXmark, faPenToSquare, faTrash } from '@fortawesome/free-solid-svg-icons'

const ClientModal = ({ show, handleClose, client }) => {
   if (!client) return null;
   
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
            <Modal.Title>Client Details</Modal.Title>
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
         <Modal.Footer
            className='Dark'
            style={{
               borderTop: "2px rgb(23, 25, 27) solid",
            }}
         >
            <Button variant="dark" onClick={handleClose}><FontAwesomeIcon icon={faPenToSquare} /> Edit</Button>
            <Button variant="danger" onClick={handleClose}><FontAwesomeIcon icon={faTrash} /> Delete</Button>
         </Modal.Footer>
      </Modal>
   );
};

export default ClientModal;
import React, { useState, useEffect } from 'react';
import { Modal, Button, Form, Spinner } from 'react-bootstrap';

import axios from 'axios';

import "./../Modal.css";

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faCheck, faXmark, faPenToSquare, faTrash, faCartFlatbed } from '@fortawesome/free-solid-svg-icons'

import { apiUrl } from '../../config.ts';

type FormData = {
   name: string;
   price: number;
   description?: string;
   category?: string;
   availabilityStatus?: string;
   photoBlob?: Uint8Array;
   quantityStock: number;
};

const ProductModal = ({ show, handleClose, product, onProductUpdated }) => {
   const [loading, setLoading] = useState(false);

   const [currentState, setCurrentState] = useState("default");
   const [status, setStatus] = useState("Out of stock");

   const [formData, setFormData] = useState<FormData>({
      name: "",
      price: 0,
      description: "",
      category: "",
      availabilityStatus: "Out of stock",
      quantityStock: 0
   });

   useEffect(() => {
      if (show) {
         setCurrentState("default");
      }
   }, [show]);

   if (!product) return null;

   const handleEdit = () => {
      setStatus(product.availabilityStatus);
      setFormData({
         name: product.name,
         price: product.price,
         description: product.description,
         category: product.category,
         availabilityStatus: product.availabilityStatus,
         quantityStock: product.quantityStock
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
            await axios.put(`${apiUrl}/product/edit/${product.id}`, {
               ...formData,
               photoBlob: formData.photoBlob || null,
               availabilityStatus: status,
            });
            onProductUpdated();

            handleClose();
         } catch (error) {
            console.error("Error updating product:", error);
         } finally {
            setLoading(false);
         }
      } else if (currentState === "delete") {
         try {
            await axios.delete(`${apiUrl}/product/remove/${product.id}`);
            onProductUpdated();

            handleClose();
         } catch (error) {
            console.error("Error deleting product:", error);
         } finally {
            setLoading(false);
         }
      }
   };

   const handleStatusChange = () => {
      if (status === "Out of stock") {
         setStatus("To order");
      }
      else if (status === "To order") {
         setStatus("In stock");
      }
      else {
         setStatus("Out of stock");
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
            <Modal.Title>Product details</Modal.Title>
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
                     <Form.Label className="me-2">Price:</Form.Label>
                     <Form.Control
                        type="text"
                        name="price"
                        value={formData.price}
                        onChange={handleInputChange}
                        placeholder="Enter price"
                     />
                  </Form.Group>
                  <Form.Group className="mb-3 d-flex">
                     <Form.Label className="me-2">Description:</Form.Label>
                     <Form.Control
                        type="text"
                        name="description"
                        value={formData.description}
                        onChange={handleInputChange}
                        placeholder="Enter description"
                     />
                  </Form.Group>
                  <Form.Group className="mb-3 d-flex">
                     <Form.Label className="me-2">Category:</Form.Label>
                     <Form.Control
                        type="text"
                        name="category"
                        value={formData.category}
                        onChange={handleInputChange}
                        placeholder="Enter category"
                     />
                  </Form.Group>
                  <Form.Group className="mb-3 d-flex">
                     <Form.Label className="me-2">Status:</Form.Label>
                     <Button variant={status === "In stock" ? "success" : status === "To order" ? "warning" : "danger"} onClick={handleStatusChange}>
                        {status === "In stock" ? (
                           <FontAwesomeIcon style={{ color: "white", marginRight: "4px" }} icon={faCheck} />
                        ) : status === "To order" ? (
                           <FontAwesomeIcon style={{ color: "rgb(27, 31, 35)", marginRight: "4px" }} icon={faCartFlatbed} />
                        ) : (
                           <FontAwesomeIcon style={{ color: "white", marginRight: "4px" }} icon={faXmark} />
                        )}
                        {status}
                     </Button>
                  </Form.Group>

                  <Form.Group className="mb-3 d-flex">
                     <Form.Label className="me-2">Quantity stock:</Form.Label>
                     <Form.Control
                        type="text"
                        name="quantityStock"
                        value={formData.quantityStock}
                        onChange={handleInputChange}
                        placeholder="Enter quantity stock"
                     />
                  </Form.Group>
               </Form>
            </Modal.Body>
         ) : (
            <Modal.Body className='Dark'>
               <h5 style={{ marginBottom: "18px" }}>Name: {product?.name}</h5>
               <p><strong>Price:</strong> {product?.price}</p>
               <p><strong>Description:</strong> {product?.description}</p>
               <p><strong>Category:</strong> {product?.category}</p>
               <p><strong style={{ marginRight: "4px" }}>Status:</strong>
                  {product.availabilityStatus === "In stock" ? (
                     <FontAwesomeIcon style={{  marginRight: "4px" }} icon={faCheck} />
                  ) : product.availabilityStatus === "To order" ? (
                     <FontAwesomeIcon style={{ marginRight: "4px" }} icon={faCartFlatbed} />
                  ) : (
                     <FontAwesomeIcon style={{ marginRight: "4px" }} icon={faXmark} />
                  )}
                  {product.availabilityStatus}
               </p>
               {product.photoBlob ? (
                  <img
                     src={`data:image/png;base64,${product.photoBlob}`}
                     className="rounded-circle"
                     alt="Product photo"
                  />
               ) : ( <></> )}
               <p><strong>Quantity stock:</strong> {product?.quantityStock}</p>
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

export default ProductModal;
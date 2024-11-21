import React, { useState, useEffect } from 'react';
import { Modal, Button, Form, Spinner } from 'react-bootstrap';

import axios from 'axios';

import "./../Modal.css";

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faCheck, faXmark, faPlus, faEraser, faCartFlatbed } from '@fortawesome/free-solid-svg-icons'

import { apiUrl } from '../../config.ts';

type Category = {
   id: string;
   name: string;
}

type FormData = {
   name: string;
   price: number;
   description?: string;
   categorys?: Category | null;
   availabilityStatus?: string;
   photoBlob?: Uint8Array;
};

const AddProductModal = ({ show, handleClose, onProductUpdated }) => {
   const [loading, setLoading] = useState(false);
   const [status, setStatus] = useState("Out of stock");

   const [selectedCategory, setSelectedCategory] = useState<string | null>(null);
   const [categories, setCategories] = useState<Category[]>([]);
   const [formData, setFormData] = useState<FormData>({
      name: "",
      price: 0,
      description: "",
      categorys: null,
      availabilityStatus: "Out of stock"
   });

   useEffect(() => {
      const fetchCategories = async () => {
         try {
            const response = await axios.get(`${apiUrl}/category`);
            setCategories(response.data);
         } catch (error) {
            console.error('Error fetching categories:', error);
         }
      };

      fetchCategories();

      if (!show) {
         handleClear();
      }
   }, [show]);

   const handleClear = () => {
      setStatus("Out of stock");
      setFormData({
         name: "",
         price: 0,
         description: "",
         categorys: null,
         availabilityStatus: "Out of stock"
      });
   };

   const handleConfirm = async () => {
      setLoading(true);

      try {
         const response = await axios.post(`${apiUrl}/product/add/`, {
            name: formData.name,
            price: formData.price,
            description: formData.description,
            categoryId: selectedCategory,
            availabilityStatus: status,
            photoBlob: null
         });

         if (onProductUpdated) {
            onProductUpdated(response.data);
         }

         handleClear();
         handleClose();
      } catch (error) {
         console.error("Error adding product:", error);
      } finally {
         setLoading(false);
      }
   };

   const handleCategoryChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
      const selectedValue = event.target.value;
      setSelectedCategory(selectedValue);
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
            <Modal.Title>Add product</Modal.Title>
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
                  <Form.Select
                     value={selectedCategory || ''}
                     onChange={handleCategoryChange}
                  >
                     <option value="" disabled>
                        Select a category
                     </option>
                     {categories.map((category) => (
                        <option key={category.id} value={category.id}>
                           {category.name}
                        </option>
                     ))}
                  </Form.Select>
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

export default AddProductModal;
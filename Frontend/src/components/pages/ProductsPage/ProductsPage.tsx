import React, { FC, useState, useEffect } from 'react';
import {
   ProductsPageWrapper,
   ProductsPageContainer,
   ButtonsContainer,
   ProductsHeaderContainer,
   ProductsHeader
} from './ProductsPage.ts';
import axios from 'axios';

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faCheck, faXmark, faArrowsRotate, faPlus, faCartFlatbed } from '@fortawesome/free-solid-svg-icons'

import { Table, Button, Spinner } from 'react-bootstrap';

import ProductModal from '../../modals/ProductModal/ProductModal.tsx';
import AddProductModal from '../../modals/AddProductModal/AddProductModal.tsx';

import { apiUrl } from '../../config.ts';

interface ProductsPageProps { }

type Category = {
   id: string;
   name: string;
}

type Product = {
   productId: string;
   name: string;
   price: number;
   description?: string;
   categorys?: Category;
   availabilityStatus?: string;
   photoBlob?: Uint8Array;
   quantity: number;
};

const ProductsPage: FC<ProductsPageProps> = () => {
   const [products, setProducts] = useState<Product[]>([]);
   const [selectedProduct, setSelectedProduct] = useState(null);

   const [loading, setLoading] = useState(false);

   const [showEditModal, setShowEditModal] = useState(false);
   const [showAddModal, setShowAddModal] = useState(false);

   const fetchProducts = async () => {
      setLoading(true);

      try {
         const response = await axios.get<Product[]>(`${apiUrl}/product/`);

         setProducts(response.data);
      } catch (error) {
         console.error('Error fetching products:', error);
      } finally {
         setLoading(false);
      }
   };

   useEffect(() => {
      fetchProducts();
   }, []);

   const updateProductList = () => {
      fetchProducts();
   };

   const handleRowClick = (product) => {
      setSelectedProduct(product);
      setShowEditModal(true);
   };

   const handleCloseEditModal = () => {
      setSelectedProduct(null);
      setShowEditModal(false);
   };

   const handleCloseAddModal = () => {
      setShowAddModal(false);
   };

   const handleAddModal = () => {
      setShowAddModal(true);
   };

   return (
      <ProductsPageWrapper>
         <ProductsPageContainer>
            <ProductsHeaderContainer>
               <ProductsHeader>Products management</ProductsHeader>
               <ButtonsContainer>
                  <Button variant="success" onClick={handleAddModal} style={{ marginRight: "12px" }}><FontAwesomeIcon icon={faPlus} /></Button>
                  <Button variant="dark" onClick={fetchProducts}><FontAwesomeIcon icon={faArrowsRotate} /></Button>
               </ButtonsContainer>
            </ProductsHeaderContainer>
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
                        <th>Price</th>
                        <th>Description</th>
                        <th>Category</th>
                        <th>Status</th>
                        <th>Quantity stock</th>
                     </tr>
                  </thead>
                  <tbody>
                     {products.map((product) => (
                        <tr
                           key={product.productId}
                           onClick={() => handleRowClick(product)}
                           style={{
                              cursor: 'pointer'
                           }}
                        >
                           <td style={{whiteSpace: "nowrap", overflow: "hidden", textOverflow: "ellipsis", maxWidth: "340px"}}>{product.name}</td>
                           <td>{product.price}</td>
                           <td style={{whiteSpace: "nowrap", overflow: "hidden", textOverflow: "ellipsis", maxWidth: "340px"}}>{product.description}</td>
                           <td>{product.categorys?.name || "No category"}</td>
                           <td>
                              {product.availabilityStatus === "In stock" ? (
                                 <FontAwesomeIcon style={{ marginRight: "4px" }} icon={faCheck} />
                              ) : product.availabilityStatus === "To order" ? (
                                 <FontAwesomeIcon style={{ marginRight: "4px" }} icon={faCartFlatbed} />
                              ) : (
                                 <FontAwesomeIcon style={{ marginRight: "4px" }} icon={faXmark} />
                              )}
                              {product.availabilityStatus}
                           </td>
                           <td>{product.quantity}</td>
                        </tr>
                     ))}
                  </tbody>
               </Table>
            )}
            <ProductModal
               show={showEditModal}
               handleClose={handleCloseEditModal}
               product={selectedProduct}
               onProductUpdated={updateProductList}
            />
            <AddProductModal
               show={showAddModal}
               handleClose={handleCloseAddModal}
               onProductUpdated={updateProductList}
            />
         </ProductsPageContainer>
      </ProductsPageWrapper >
   );
};

export default ProductsPage;
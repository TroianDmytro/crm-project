import React, { FC, useState, useEffect } from 'react';
import {
   DealsPageWrapper,
   DealsPageContainer,
   ButtonsContainer,
   DealsHeaderContainer,
   DealsHeader
} from './DealsPage.styled.ts';
import axios from 'axios';

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faCheck, faXmark, faArrowsRotate, faPlus, faClockRotateLeft } from '@fortawesome/free-solid-svg-icons'

import { Table, Button, Spinner } from 'react-bootstrap';

import DealModal from '../../modals/DealModal/DealModal.tsx';
import AddDealModal from '../../modals/AddDealModal/AddDealModal.tsx';

import { apiUrl } from '../../config.ts';

interface DealsPageProps { }

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


type Deal = {
   dealId: string;
   title: string;
   amount: number;
   expectedCloseDate: string;
   status: string;
   createdAt: string;
   client: Client;
};

const DealsPage: FC<DealsPageProps> = () => {
   const [deals, setDeals] = useState<Deal[]>([]);
   const [selectedDeal, setSelectedDeal] = useState(null);

   const [loading, setLoading] = useState(false);

   const [showEditModal, setShowEditModal] = useState(false);
   const [showAddModal, setShowAddModal] = useState(false);

   const [hoveredRow, setHoveredRow] = useState<string | null>(null);

   const fetchDeals = async () => {
      setLoading(true);

      try {
         const response = await axios.get<Deal[]>(`${apiUrl}/deal/`);

         setDeals(response.data);
      } catch (error) {
         console.error('Error fetching deals:', error);
      } finally {
         setLoading(false);
      }
   };

   useEffect(() => {
      fetchDeals();
   }, []);

   const updateDealList = () => {
      fetchDeals();
   };

   const handleRowClick = (deal) => {
      setSelectedDeal(deal);
      setShowEditModal(true);
   };

   const handleCloseEditModal = () => {
      setSelectedDeal(null);
      setShowEditModal(false);
   };

   const handleCloseAddModal = () => {
      setShowAddModal(false);
   };

   const handleAddModal = () => {
      setShowAddModal(true);
   };

   return (
      <DealsPageWrapper>
         <DealsPageContainer>
            <DealsHeaderContainer>
               <DealsHeader>Deals management</DealsHeader>
               <ButtonsContainer>
                  <Button variant="success" onClick={handleAddModal} style={{ marginRight: "12px" }}><FontAwesomeIcon icon={faPlus} /></Button>
                  <Button variant="dark" onClick={fetchDeals}><FontAwesomeIcon icon={faArrowsRotate} /></Button>
               </ButtonsContainer>
            </DealsHeaderContainer>
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
                        <th>Title</th>
                        <th>Amount</th>
                        <th>Close date</th>
                        <th>Status</th>
                        <th>Created at</th>
                        <th>Client</th>
                     </tr>
                  </thead>
                  <tbody>
                     {deals.map((deal) => (
                        <tr
                           key={deal.dealId}
                           onClick={() => handleRowClick(deal)}
                           onMouseEnter={() => setHoveredRow(deal.dealId)}
                           onMouseLeave={() => setHoveredRow(null)}
                           style={{
                              cursor: 'pointer'
                           }}
                        >
                           <td>{deal.title}</td>
                           <td>{deal.amount}</td>
                           <td>{new Date(deal.expectedCloseDate).toLocaleString()}</td>
                           <td>
                              {deal.status === "New" ? (
                                 <FontAwesomeIcon style={{ marginRight: "4px" }} icon={faPlus} />
                              ) : deal.status === "In process" ? (
                                 <FontAwesomeIcon style={{ marginRight: "4px" }} icon={faClockRotateLeft} />
                              ) : (
                                 <FontAwesomeIcon style={{ marginRight: "4px" }} icon={faCheck} />
                              )}
                              {deal.status}
                           </td>
                           <td>{new Date(deal.createdAt).toLocaleString()}</td>
                           <td>{deal.client.name} {deal.client.lastName}</td>
                        </tr>
                     ))}
                  </tbody>
               </Table>
            )}
            <DealModal
               show={showEditModal}
               handleClose={handleCloseEditModal}
               deal={selectedDeal}
               onDealUpdated={updateDealList}
            />
            <AddDealModal
               show={showAddModal}
               handleClose={handleCloseAddModal}
               onDealUpdated={updateDealList}
            />
         </DealsPageContainer>
      </DealsPageWrapper >
   );
};

export default DealsPage;

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
   patronymic: string;
   userName: string;
   address: string;
   dateOfBirth: string;
   hireDate: string;
   position: string;
   department: string;
   email: string;
   password: string;
};

const AddManagerModal = ({ show, handleClose, onManagerUpdated }) => {


   return (
      <Modal
         show={show} onHide={handleClose} centered size="lg" backdrop="static"
      >

      </Modal>
   );
};

export default AddManagerModal;
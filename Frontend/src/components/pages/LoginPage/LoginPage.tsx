import React, { FC, useState } from 'react';
import {
   LoginPageWrapper,
   LoginPageContainer
} from './LoginPage.styled.ts';
import './LoginPage.css';
import axios from 'axios';

import { Form, Button, Container } from "react-bootstrap";
import 'bootstrap/dist/css/bootstrap.min.css';

import { apiUrl } from '../../config.ts';

interface LoginPageProps { }

const LoginPage: FC<LoginPageProps> = () => {
   const [username, setUsername] = useState("");
   const [password, setPassword] = useState("");

   const handleSubmit = async (e: React.FormEvent) => {
      e.preventDefault();
   
      try {
         const response = await axios.post(`${apiUrl}/auth/login/`, {
            UserName: username,
            Password: password
         });
   
         const token = response.data.token;
   
         console.log(token);
         if (token) {
            localStorage.setItem('authToken', token);
         }

         //TODO запис імені та ролей у глобальні змінні (коли бек буде)
      } catch (error) {
         console.error("Error during login:", error);
         alert("Login failed. Please try again.");
      }
   };

   return (
      <LoginPageWrapper>
         <LoginPageContainer>
            <Form onSubmit={handleSubmit} style={{ width: "300px" }}>
               <Form.Group controlId="formUsername" className="mb-3">
                  <Form.Control
                     className='FormPlaceholder'
                     style={{
                        backgroundColor: "rgb(33, 37, 41)",
                        color: "white",
                        border: "none"
                     }}
                     type="text"
                     placeholder="Enter username"
                     value={username}
                     onChange={(e) => setUsername(e.target.value)}
                     required
                  />
               </Form.Group>
               <Form.Group controlId="formPassword" className="mb-3">
                  <Form.Control
                     className='FormPlaceholder'
                     style={{
                        backgroundColor: "rgb(33, 37, 41)",
                        color: "white",
                        border: "none"
                     }}
                     type="password"
                     placeholder="Enter password"
                     value={password}
                     onChange={(e) => setPassword(e.target.value)}
                     required
                  />
               </Form.Group>
               <Button className="w-100" variant="dark" type="submit">
                  Login
               </Button>
            </Form>
         </LoginPageContainer>
      </LoginPageWrapper>
   );
};

export default LoginPage;

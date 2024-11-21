import React, { FC, useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import {
   PageHeaderWrapper,
   HeaderContainer
} from './PageHeader.styled.ts';

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faRightToBracket, faUserTag, faHandshakeSimple, faServer, faUserShield, faUserPen, faDolly } from '@fortawesome/free-solid-svg-icons'

import { Container, Navbar, Nav } from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';

interface PageHeaderProps { }

const PageHeader: FC<PageHeaderProps> = () => {
   const [loggedInAs, setLoggedInAs] = useState<string | null>(null);
   const [nickname, setNickname] = useState<string | null>(null);

   useEffect(() => {
      const handleLoginUpdate = () => {
         setLoggedInAs((window as any).loggedIn);
         setNickname((window as any).nickname);
      };

      handleLoginUpdate();

      window.addEventListener("loggedIn", handleLoginUpdate);
   }, []);

   return (
      <PageHeaderWrapper>
         <HeaderContainer>
            <Navbar bg="dark" data-bs-theme="dark" style={{ width: "100%", borderRadius: "12px" }}>
               <Container>
                  <Navbar.Brand href=" "><FontAwesomeIcon icon={faServer} /> CRM</Navbar.Brand>
                  <Nav className="me-auto">
                     {!loggedInAs && (
                        <Nav.Link as={Link} to="/Login"><FontAwesomeIcon icon={faRightToBracket} /> Login</Nav.Link>
                     )}
                     {loggedInAs && (
                        <>
                           <Nav.Link as={Link} to="/Clients"><FontAwesomeIcon icon={faUserTag} /> Clients</Nav.Link>
                           <Nav.Link as={Link} to="/Deals"><FontAwesomeIcon icon={faHandshakeSimple} /> Deals</Nav.Link>
                        </>
                     )}
                     {(loggedInAs === "boss" || loggedInAs === "admin") && (
                        <>
                           <Nav.Link as={Link} to="/Products"><FontAwesomeIcon icon={faDolly} /> Products</Nav.Link>
                           <Nav.Link as={Link} to="/Managers"><FontAwesomeIcon icon={faUserPen} /> Managers</Nav.Link>
                        </>
                     )}
                     {loggedInAs === "boss" && (
                        <Nav.Link as={Link} to="/Admins"><FontAwesomeIcon icon={faUserShield} /> Admins</Nav.Link>
                     )}
                  </Nav>
                  {loggedInAs && (
                     <Navbar.Collapse className="justify-content-end">
                        <Navbar.Text>
                           Signed in as: <a style={{ color: "white", marginRight: "18px" }}>{nickname}</a>
                        </Navbar.Text>
                     </Navbar.Collapse>
                  )}
               </Container>
            </Navbar>
         </HeaderContainer>
      </PageHeaderWrapper>
   );
};

export default PageHeader;

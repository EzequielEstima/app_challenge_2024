import './App.css';
import { BrowserRouter, Routes, Route } from "react-router-dom";
import { ListTickets } from './listTickets/listTickets';
import { ShowTicketDetails } from './showTicketDetails/showTicketDetails';
import { Layout } from './layout';
import { EditTicket } from './editTicket/editTicket';
import { CreateTicketForm1 } from './createTicketForm/createTicketForm';
import { HomePage } from './homePage/homePage';


export function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Layout />}>
          <Route index element={<HomePage />} />
          <Route path="tickets/new" element={<CreateTicketForm1 />} />
          <Route path="tickets" element={<ListTickets />} />
          <Route path="tickets/:id" element={<ShowTicketDetails />} />
          <Route path="tickets/:id/edit" element={<EditTicket />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

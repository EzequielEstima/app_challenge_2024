import { useEffect, useState } from "react";
import { Ticket } from "../../dataModels/Ticket";
import { TableContainer, Paper, Table, TableHead, TableRow, TableCell, TableBody, Button } from "@mui/material";
import { useNavigate } from 'react-router-dom';
import { TicketService } from "../../service/ticketService";
import { ProdutoService } from "../../service/produtoService";
import { Produto } from "../../dataModels/Produto";


export function ListTickets() {
  const navigate = useNavigate();

  const [tickets, setTickets] = useState<Ticket[]>([]);
  const [products, setProducts] = useState<Produto[]>([]);

  useEffect( () => {
    const ticketService = new TicketService();
    ticketService.getTickets().then((tickets) => setTickets(tickets));

    const productServer = new ProdutoService();

    productServer.getProdutos().then((produtos) => {debugger; console.log(produtos); setProducts([...produtos]?? [])});
  }, []); // No dependencies, so it only runs once at the start
  
  function handleDetalhesClick(ticket: Ticket) {
    navigate('/tickets/' + ticket.id);
  }

  return (
    <TableContainer component={Paper}>
      <Table sx={{ minWidth: 650 }} aria-label="simple table">
        <TableHead>
          <TableRow>
            <TableCell>Título</TableCell>
            <TableCell>Produto</TableCell>
            <TableCell></TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {tickets.map((ticket) => (
            <TableRow
            key={ticket.titulo}
            sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
            >
              <TableCell component="th" scope="row">{ticket.titulo}</TableCell>
              <TableCell>{products.find((produto) => produto.id === ticket.produtoId)?.nome}</TableCell>
              <TableCell>
                <Button variant="contained" onClick={() => handleDetalhesClick(ticket) }>Detalhes</Button>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
}
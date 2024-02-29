import { useEffect, useState } from "react";
import { Ticket } from "../../dataModels/Ticket";
import { TableContainer, Paper, Table, TableHead, TableRow, TableCell, TableBody, Button, styled, tableCellClasses } from "@mui/material";
import { useNavigate } from 'react-router-dom';
import { TicketService } from "../../service/ticketService";
import { ProdutoService } from "../../service/produtoService";
import { Produto } from "../../dataModels/Produto";

const StyledTableCell = styled(TableCell)(({ theme }) => ({
  [`&.${tableCellClasses.head}`]: {
    backgroundColor: theme.palette.primary.main,
    color: theme.palette.common.white,
  },
  [`&.${tableCellClasses.body}`]: {
    fontSize: 14,
  },
}));

const StyledTableRow = styled(TableRow)(({ theme }) => ({
  '&:nth-of-type(odd)': {
    backgroundColor: theme.palette.action.hover,
  },
  // hide last border
  '&:last-child td, &:last-child th': {
    border: 0,
  },
}));


export function ListTickets() {
  const navigate = useNavigate();

  const [tickets, setTickets] = useState<Ticket[]>([]);
  const [products, setProducts] = useState<Produto[]>([]);

  useEffect( () => {
    const ticketService = new TicketService();
    ticketService.getTickets().then((listaticketsModel) => setTickets(listaticketsModel.tickets));

    const productService = new ProdutoService();

    productService.getProdutos().then((listaProdutosModel) => {setProducts(listaProdutosModel.products)});

  }, []); // No dependencies, so it only runs once at the start
  
  function handleDetalhesClick(ticket: Ticket) {
    navigate(`/tickets/${ticket.ticketId}`);
  }

  return (
    <TableContainer component={Paper}>
      <Table sx={{ minWidth: 650 }} aria-label="simple table">
        <TableHead>
          <TableRow>
            <StyledTableCell>Título</StyledTableCell>
            <StyledTableCell>Produto</StyledTableCell>
            <StyledTableCell></StyledTableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {tickets.map((ticket) => (
            <StyledTableRow
            key={ticket.titulo}
            sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
            >
              <StyledTableCell component="th" scope="row">{ticket.titulo}</StyledTableCell>
              <StyledTableCell>{products.find((produto) => produto.productId === ticket.produtoId)?.nome}</StyledTableCell>
              <StyledTableCell>
                <Button variant="contained" onClick={() => handleDetalhesClick(ticket) }>Detalhes</Button>
              </StyledTableCell>
            </StyledTableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
}
import { useEffect, useState } from "react";
import { Ticket } from "../../dataModels/Ticket";
import { TableContainer, Paper, Table, TableHead, TableRow, TableCell, TableBody, Button, styled, tableCellClasses, Stack, IconButton, ButtonGroup } from "@mui/material";
import { useNavigate } from 'react-router-dom';
import { TicketService } from "../../service/ticketService";
import { ProdutoService } from "../../service/produtoService";
import { Produto } from "../../dataModels/Produto";
import KeyboardArrowLeftIcon from '@mui/icons-material/KeyboardArrowLeft';
import KeyboardArrowRightIcon from '@mui/icons-material/KeyboardArrowRight';

import './listTickets.css';
import { get } from "http";

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

const abortController = new AbortController();

export function ListTickets() {
  const navigate = useNavigate();

  const [tickets, setTickets] = useState<Ticket[]>([]);
  const [products, setProducts] = useState<Produto[]>([]);
  const [page, setPage] = useState(0);
  const [pageLength, setPageLength] = useState(5);
  const [totalItems, setTotalItems] = useState(0);

  useEffect( () => {
    const productService = new ProdutoService();

    productService.getProdutos(abortController)
    .then((listaProdutosModel) => setProducts(listaProdutosModel.products))
    .catch((error) =>{
      if (error.code === 'ERR_NETWORK') {
        abortController.abort();
        alert('Não foi possível ligar ao servidor');
        navigate('/');
      }else if(error.code !== "ERR_CANCELED"){
        alert(`Não foi possível obter a lista de produtos \n${error.response.data}`)
        navigate('/')
      }
    })

  }, []); // No dependencies, so it only runs once at the start

  useEffect(() => {
    getTickets(page, pageLength);
  }, [page, pageLength]);
  
  function handleDetalhesClick(ticket: Ticket) {
    navigate(`/tickets/${ticket.ticketId}`);
  }

  function onLeftArrowClick() {
    if(page > 0){
      setPage(page - 1);
    }
  }

  function onRightArrowClick() {
    if(page*pageLength + pageLength < totalItems){
      setPage(page + 1);
    }
  }

  function getTickets(page: number, pageLength : number) {
    const ticketService = new TicketService();
    ticketService.getTickets({page:page, pageLength:pageLength},abortController)
    .then((listaticketsModel) => {
      setTickets(listaticketsModel.items)
      setTotalItems(listaticketsModel.totalItems)
    })
    .catch((error) => {
      if (error.code === 'ERR_NETWORK') {
        alert('Não foi possível ligar ao servidor');
        abortController.abort();
        navigate('/')
      }else if(error.code !== "ERR_CANCELED"){
        alert(`Não foi possível obter a lista de tickets \n${error.response.data}`)
        navigate('/')
      }        
    });
  }

  return (
    <Stack width={1000}>
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
      <div className="btn-grp">
        Showing {page*pageLength} to {page*pageLength + pageLength} of {totalItems} items
        <IconButton onClick={onLeftArrowClick}>
          <KeyboardArrowLeftIcon />
        </IconButton>
        {page+1}
        <IconButton onClick={onRightArrowClick}>
          <KeyboardArrowRightIcon />
        </IconButton>
      </div>
    </Stack>
  );
}
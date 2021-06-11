using AutoMapper;
using DevIO.Api.ViewsModels;
using DevIO.Business.Intefaces;
using DevIO.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.Api.Controllers
{
    [Route("api/produtos")]
    public class ProdutosController : MainController
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;

        public ProdutosController(INotificador notificador, 
                                  IProdutoRepository produtoRepository, 
                                  IProdutoService produtoService,
                                  IMapper mapper) : base(notificador)
        {
            _produtoRepository = produtoRepository;
            _produtoService = produtoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoViewModel>>> ObterTodos()
        {
            var produto = _mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterProdutosFornecedores());
            return Ok(produto);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProdutoViewModel>> ObterPorId(Guid id)
        {
            var produto = await ObterProduto(id);

            if (produto == null) return NotFound();

            return Ok(produto);

        }

        public async Task<ProdutoViewModel> ObterProduto(Guid id)
        {
            return _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterProdutoFornecedor(id));
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ProdutoViewModel>> Excluir(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);

            if (produtoViewModel == null) return NotFound();

            await _produtoService.Remover(id);

            return CustomResponse();
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoViewModel>> Adicionar(ProdutoViewModel produtoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var imagemNome = Guid.NewGuid() + "_" + produtoViewModel.Imagem;

            if (!UploadArquivo(produtoViewModel.ImagemUpload, imagemNome)) return CustomResponse();
            
            produtoViewModel.Imagem = imagemNome;
            await _produtoService.Adicionar(_mapper.Map<Produto>(produtoViewModel));

            return CustomResponse(produtoViewModel);
        }

        private bool UploadArquivo(string arquivo,string imgNome)
        {
            if(string.IsNullOrEmpty(arquivo))
            {
                //ModelState.AddModelError(string.Empty, "Forneça uma imagem para este produto!");
                NotificarErro("Forneça uma imagem para este produto!");
                return false;
            }

            var imageDataByteArray = Convert.FromBase64String(arquivo);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", imgNome);

            if(System.IO.File.Exists(filePath))
            {
                //ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
                NotificarErro("Já existe um arquivo com este nome!");
                return false;
            }

            //gerar o arquivo final
            System.IO.File.WriteAllBytes(filePath, imageDataByteArray);

            return true;

        }



    }
}

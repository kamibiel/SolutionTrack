using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SolutionTrack.Dominio.ModelViews
{
    public struct Home
    {
        public string Mensagem
        {
            get => "👋 Bem-vindo à API SolutionTrack! Desenvolvida pela BlackBit para ajudar a rastrear e solucionar problemas. Explore os endpoints para descobrir mais funcionalidades!";
        }
        public string Doc
        {
            get => "/swagger";
        }
        public string Empresa { get => "BlackBit"; }
        public string Versao { get => "Versão 1.0.0"; }
        public string Autor { get => "Desenvolvido por KamiBiel"; }
    }
}
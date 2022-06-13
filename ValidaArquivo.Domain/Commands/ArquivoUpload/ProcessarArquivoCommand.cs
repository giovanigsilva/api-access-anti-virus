using Microsoft.AspNetCore.Http;

namespace ValidaArquivo.Domain.Commands.ArquivoUpload
{
    /// <summary>
    /// Comando para upload e processamento dos arquivos
    /// </summary>
    public class ProcessarArquivoCommand
    {
        public ProcessarArquivoCommand()
        {
            Files = new List<File>();
        }

        /// <summary>
        /// Arquivos
        /// </summary>
        public List<File> Files { get; set; }

        /// <summary>
        /// Retorna a lista de arquivos enviados via POST
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static ProcessarArquivoCommand FromCollection(IFormCollection collection)
        {
            var command = new ProcessarArquivoCommand();
            foreach (var postFile in collection.Files)
            {
                File file = new File();

                file.ContentType = postFile.ContentType;
                file.FileName = postFile.FileName;

                using (var ms = new MemoryStream())
                {
                    postFile.CopyTo(ms);
                    file.Content = ms.ToArray();
                }

                command.Files.Add(file);
            }
            return command;
        }
    }

    /// <summary>
    /// Classe para representar o arquivo enviado
    /// </summary>
    public class File
    {
        /// <summary>
        /// Nome do arquivo
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Tipo MIME de arquivo
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Conteúdo em bytes do arquivo
        /// </summary>
        public byte[] Content { get; set; }
    }
}

namespace SP07
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string[] fileNames = { "file1.txt", "file2.txt", "file3.txt", "file4.txt" };
            string[] encryptedFileNames = { "encrypted_file1.txt", "encrypted_file2.txt", "encrypted_file3.txt", "encrypted_file4.txt" };

            await CreateTestFilesAsync(fileNames);
            Task[] tasks = new Task[fileNames.Length];
            for (int i = 0; i < fileNames.Length; i++)
            {
                tasks[i] = EncryptFileAsync(fileNames[i], encryptedFileNames[i]);
            }

            await Task.WhenAll(tasks);

            Console.WriteLine("Шифрование завершено.");
        }

        public static async Task EncryptFileAsync(string inputFilePath, string outputFilePath)
        {
            using (FileStream inputFileStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read))
            using (FileStream outputFileStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
            {
                byte[] buffer = new byte[4096];
                int bytesRead;

                while ((bytesRead = await inputFileStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    
                    for (int i = 0; i < bytesRead; i++)
                    {
                        buffer[i] += 1;
                    }
                    await outputFileStream.WriteAsync(buffer, 0, bytesRead);
                }
            }
        }


        public static async Task CreateTestFilesAsync(string[] fileNames)
        {
            foreach (var fileName in fileNames)
            {
                string content = "Hello from " + fileName; 
                await File.WriteAllTextAsync(fileName, content);
                Console.WriteLine($"Создан файл: {fileName}");
            }
        }
    }
}

using DocRaptor.Client;
using DocRaptor.Model;
using DocRaptor.Api;
using System;
using System.Threading;

class AsyncTest {
  static void Main(string[] args) {
    Configuration.Default.Username = "YOUR_API_KEY_HERE";
    // Configuration.Default.Debug = true; // Not supported in Csharp
    DocApi docraptor = new DocApi();

    Doc doc = new Doc();
    doc.Name = "csharp-async.pdf";
    doc.Test = true;
    doc.DocumentContent = "<html><body>Hello from C#</body></html>";
    doc.DocumentType = "pdf";

    AsyncDoc response = docraptor.CreateAsyncDoc(doc);

    AsyncDocStatus status_response;
    while(true) {
      status_response = docraptor.GetAsyncDocStatus(response.StatusId);
      if (status_response.Status == "completed") {
        break;
      }
      Thread.Sleep(1000);
    }

    docraptor.GetAsyncDoc(status_response.DownloadId);
  }
}

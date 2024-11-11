using Microsoft.AspNetCore.Mvc;
using Opc.UaFx.Client;
using OPC_Web.Services;

namespace OPC_Web.Controllers
{
    public class OPC_UA_Controller : Controller
    {
        private readonly OPC_UA_Service _opcService;
        private string serverUrl = "opc.tcp://172.26.134.121:49320";
        public OPC_UA_Controller(OPC_UA_Service opcService)
        {
            _opcService = opcService;
        }

        [HttpPost]
        public IActionResult Connect(string? ServerUrl)
        {
            try
            {
                if (ServerUrl != null)
                {
                    serverUrl = ServerUrl;
                }
                _opcService.Connect(serverUrl);
                ViewBag.StatusMessage = "Connected to OPC UA server.";

            }
            catch (System.Exception ex)
            {
                ViewBag.StatusMessage = "Error: " + ex.Message;
            }
            return View("Index");

        }

        [HttpPost]
        public IActionResult Read(string nodeId)
        {
            try
            {
                var value = _opcService.ReadNode(nodeId);
                ViewBag.StatusMessage = $"Read Value: {value}";
            }
            catch (Exception ex)
            {
                ViewBag.StatusMessage = $"Error reading: {ex.Message}";
            }
            return View("Index");
        }

        [HttpPost]
        public IActionResult Write(string nodeId, string value)
        {
            try
            {
                _opcService.WriteNode(nodeId, value);
                ViewBag.StatusMessage = $"Wrote Value: {value}";
            }
            catch (Exception ex)
            {
                ViewBag.StatusMessage = $"Error writing: {ex.Message}";
            }
            return View("Index");
        }

        [HttpPost]
        public IActionResult Disconnect()
        {
            try
            {
                _opcService.Disconnect();
                ViewBag.StatusMessage = "Disconnected from OPC UA server.";
            }
            catch (Exception ex)
            {
                ViewBag.StatusMessage = "Error: " + ex.Message;
            }
            return View("Index");
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}

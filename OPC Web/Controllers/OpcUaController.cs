using Microsoft.AspNetCore.Mvc;
using Opc.UaFx.Client;
using OPC_Web.Services;

namespace OPC_Web.Controllers
{
    public class OpcUaController : Controller
    {
        private readonly OpcUaService _opcService;
        private string serverUrl = "opc.tcp://172.26.134.121:49320";
        public OpcUaController(OpcUaService opcService)
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
        public IActionResult Write(string nodeId, string writeValue)
        {
            try
            {
                _opcService.WriteNode(nodeId, writeValue);
                ViewBag.StatusMessage = $"Wrote Value: {writeValue}";
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

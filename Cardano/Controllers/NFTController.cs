using Cardano;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class NFTController : ControllerBase
{
    private readonly CardanoService _cardanoService;

    private const string jamonBreadAddress = "addr1wxppkjlh4t6g60nyrntsx40cdh5l76l3a7te2z5trjth98qc6mflw";
    private const string jpgStoreAddress = "addr1zxgx3far7qygq0k6epa0zcvcvrevmn0ypsnfsue94nsn3tvpw288a4x0xf8pxgcntelxmyclq83s0ykeehchz2wtspks905plm";

    public NFTController()
    {
        _cardanoService = new CardanoService();
    }

    [HttpGet("count/{policyId}")]
    public async Task<IActionResult> GetNFTCount(string policyId)
    {
        int jamonBreadCount = await _cardanoService.GetNFTCountByPolicyId(policyId, jamonBreadAddress);
        int jpgStoreCount = await _cardanoService.GetNFTCountByPolicyId(policyId, jpgStoreAddress);

        int total;
        if (jamonBreadCount != -1 && jpgStoreCount != -1)
        {
            total = jamonBreadCount + jpgStoreCount;
        }
        else
        {
            total = -1;
        }

        return Ok(new
        {
            policyId,
            jamonbread_io = jamonBreadCount,
            jpg_store = jpgStoreCount,
            total
        });
    }
}
using Flurl.Http;

namespace Cardano
{
    public class CardanoService
    {
        private const string DandelionApiUrl = "https://graphql-api.mainnet.dandelion.link/";

        public async Task<int> GetNFTCountByPolicyId(string policyId, string scriptAddress)
        {

            var query = $@"query {{
                            utxos_aggregate(where: {{
                                address: {{_eq: ""{scriptAddress}""}},
                                tokens: {{asset: {{policyId: {{_eq: ""{policyId}""}}}}}}
                            }}) {{
                                aggregate {{
                                    count
                                }}
                            }}
                        }}";
            try
            {
                var response = await DandelionApiUrl
                    .PostJsonAsync(new { query })
                    .ReceiveJson();

                var count = Convert.ToInt32(response.data.utxos_aggregate.aggregate.count.ToString());
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching data from Dandelion API:");
                Console.WriteLine(ex.ToString());
                return -1;
            }
        }
    }
}

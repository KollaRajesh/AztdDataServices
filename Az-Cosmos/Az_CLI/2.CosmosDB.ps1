## creating CosmosDB 
## --default-consistency-level {BoundedStaleness, ConsistentPrefix, Eventual, Session, Strong}]

$location1="eastus"
$location2="centralus"

$randomIdentifier="aztd"

$resourceGroup="cosmosrg-$randomIdentifier"

$account1="cosaccount1-$randomIdentifier"
$account2="cosaccount2-$randomIdentifier"

$database="database-$randomIdentifier"

$failoverGroup= "failovergroup-$randomIdentifier"

$secondayDB="secondarydb-$randomIdentifier"

$dbName ="cosmosdb-$randomIdentifier"
$ContainerName= "posts"


echo "Creating  resource group $resourceGroup..."
az group create `
                 --name $resourceGroup `
                 --location "$location"


az cosmosdb create `
                    --name $account1 `
                    --resource-group $resourceGroup `
                    ##--subscription MySubscription

## create two write locations 
#az cosmosdb create `
#                  -n $account2 `
#                  -g $resourceGroup `
#                  --locations regionName=$location1 failoverPriority=0 isZoneRedundant=False `
#                  --locations regionName=$location2 failoverPriority=1 isZoneRedundant=True `
#                  --enable-multiple-write-locations

az cosmosdb list-connection-strings `
                        --name $account1  `
                        --resource-group $resourceGroup

echo "cosmos database list in resource group $resourceGroup"
az cosmosdb database list --name $account1 -g $resourceGroup

echo "creating cosmos database in account $account1"

$dbExists=az cosmosdb sql database exists --db-name $dbName --name $account1 -g $resourceGroup
if ( $dbExists -ne $true ){
az cosmosdb sql database create --name $account1 -g $resourceGroup --db-name $dbName 
}                       

echo "get cosmos database $dbName in account $account1"
az cosmosdb sql database show `
                            --db-name $dbName `
                            --name $account1 `
                            
                            -g $resourceGroup

echo "create container $dbName in database $dbName and account $account1 and resource group  $resourceGroup"
az cosmosdb sql container create `
                                -g $resourceGroup `
                                -a $account1 `
                                -d $dbName `
                                -n $ContainerName `
                                --partition-key-path "/my/id" `
                                --idx @policy-file.json `
                                --ttl 1000 `
                                --throughput "700"

az cosmosdb sql container show  `
                                --account-name $account1 `
                                --database-name $dbName `
                                --name $ContainerName
                               
##echo "Cleaning Resources in resource group $resourceGroup ..."
##az group delete --name $resourceGroup --no-wait


$location="eastus"

$randomIdentifier="aztd"

$resourceGroup="learn-af6ceeb5-e4bd-4917-9d62-58242c591da1" ##"rg-$randomIdentifier"

$server="sqlserver-$randomIdentifier"

$database="sqldb-$randomIdentifier"

$failoverGroup= "failovergroup-$randomIdentifier"

$secondayDB="secondarydb-$randomIdentifier"

$sqlepoolDB="sqlepool-$randomIdentifier"


$c= Get-Credential

$login=$c.UserName
$password=[System.Runtime.InteropServices.Marshal]::PtrToStringAuto([System.Runtime.InteropServices.Marshal]::SecureStringToBSTR($c.Password))

$startIP="0.0.0.0"
$endIP="0.0.0.0"

echo "Creating  resource group $resourceGroup..."
az group create `
                 --name $resourceGroup `
                 --location "$location"

echo "Creating SQL Server $server in $location..."

az sql server create `
                    --name $server `
                    --resource-group $resourceGroup `
                    --location "$location" `
                    --admin-user $login `
                    --admin-password $password


echo "list all  SQL Servers  in $resourceGroup..."
az sql server list `
                -g $resourceGroup


echo "Configuring firewall..."
az sql server firewall-rule create `
                            -g $resourceGroup `
                            -s $server `
                            -n AllowClientIp `
                            --start-ip-address $startIP `
                            --end-ip-address $endIP


echo "Creating $database on $server..."
az sql db create `
                --resource-group $resourceGroup `
                --server $server `
                --name $database  `
                --sample-name AdventureWorksLT `
                --edition Basic `
                --family Gen4  `
                --capacity 1 `
                --zone-redundant false # zone redundancy is only supported on premium and business critical service tiers

echo "list all databases in $server server and $resourceGroup resourceGroup..."
az sql db list `
                --resource-group $resourceGroup `
                --server $server `
                  
echo "show database details of $database on  $server..."
az sql db show `
                --resource-group $resourceGroup `
                --server $server `
                --name $database  `


echo "Creating failover-group $failoverGroup on $server..."

az sql failover-group create `
                            --name $failoverGroup `
                            --partner-server $secondayDB `
                            --resource-group $resourceGroup `
                            --server $server `
                            ##--subscription MySubscription


echo "setting primaty db in failover-group $failoverGroup on $server..."
az sql failover-group set-primary `
                                   --name $failoverGroup `
                                   --resource-group $secondayDB `
                                   --server $server


##Azure SQL Database elastic pools are a simple,
## cost-effective solution for managing and scaling multiple databases that have varying and unpredictable usage demands. 
## The databases in an elastic pool are on a single server and share a set number of resources at a set price

echo "creat SQLdb in elastic pool on $server..."

az sql elastic-pool create `
                            -g $resourceGroup `
                            -s $server `
                            -n $sqlepoolDB `
                            -e Standard -c 100
                            ##-e GeneralPurpose -f Gen4 -c 1

##echo "Cleaning Resources in resource group $resourceGroup ..."
##az group delete --name $resourceGroup --no-wait


         

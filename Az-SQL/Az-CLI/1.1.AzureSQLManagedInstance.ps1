$location="eastus"

$randomIdentifier="aztd"

$resourceGroup="rg-$randomIdentifier"

$server="sqlmiserver-$randomIdentifier"

$db="sqlmidb-$randomIdentifier"

$failoverGroup= "sqlmifailovergroup-$randomIdentifier"

$partnerDb="sqlmisecondarydb-$randomIdentifier"

$sqlepoolDB="sqlepool-$randomIdentifier"

$edition="GeneralPurpose"
$pool_name="sqlinstanceool-$randomIdentifier"


$c= Get-Credential

$userName=$c.UserName
$password=[System.Runtime.InteropServices.Marshal]::PtrToStringAuto([System.Runtime.InteropServices.Marshal]::SecureStringToBSTR($c.Password))

echo "creating SQL managed instance"
az sql mi create `
                  -g $resourceGroup  `
                  -n $db `
                  -l $location `
                  -i -u $userName -p $password `
                  --license-type LicenseIncluded `
                  --capacity 8 `
                  --storage 32GB `
                  --edition GeneralPurpose `
                  --family Gen5


##az sql mi create -n $db -g $resourceGroup -l $location -i -u $userName -p $password --license-type LicenseIncluded  --capacity 8 --storage 32GB --edition GeneralPurpose --family Gen5 --backup-storage-redundancy Local

echo "listing sql managed instances in $resourceGroup"

az sql mi list -g $resourceGroup

echo "showing  sql managed instance $db details"
az sql mi show `
                -n $db `
                -g $resourceGroup

echo "creating mi failover group"
az sql instance-failover-group create --mi $db `
                                      --name  $failoverGroup `
                                      --partner-mi $partnerDb `
                                      --partner-resource-group $resourceGroup `
                                      --resource-group $resourceGroup `
                                      ##[--failover-policy {Automatic, Manual}] ## [--grace-period]

echo "setting primary in instance fail-over group"
az sql instance-failover-group set-primary --name $db `
                                          --location $location  `
                                          --resource-group $resourceGroup

echo "showing instance fail-over group $failoverGroup details"

az sql instance-failover-group show `
                                    --location $location `
                                    --name $failoverGroup `
                                    --resource-group  $resourceGroup


echo "Failover a managed instance primary replica"
az sql mi failover --resource-group $resourceGroup --name $db

echo "Failover a managed instance readable secodary replica"
az sql mi failover -g mygroup -n $partnerDb --replica-type ReadableSecondary


##Instance pools is a new managed instance option that provides a convenient and cost-effective way to migrate smaller instances to the cloud at scale,
##reducing or eliminating the extra work of consolidating less compute-intensive workloads.

##Instance pools allow scaling compute and storage independently.
## Customers pay for compute associated with the pool resource measured in vCores, and 
##storage associated with every instance measured in gigabytes (the first 32 GB are free of charge for every instance).

echo "Creating instance pool"
az sql instance-pool create `
                -g $resourceGroup `
                -n $pool_name `
                -l $location `
                --license-type LicenseIncluded  `
                --capacity 8 `
                -e $edition `
                -f Gen5 --no-wait

echo "list instance pool in resource group $resourceGroup"              
az sql instance-pool list -g $resourceGroup

echo "get instance pool details  instance pool"
az sql instance-pool show `
                         -g $resourceGroup `
                         -n $pool_name

##echo "Cleaning Resources in resource group $resourceGroup ..."
##az group delete --name $resourceGroup --no-wait
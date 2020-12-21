$location="eastus"

$randomIdentifier="aztd"

$resourceGroup="rg-$randomIdentifier"

$sqlvm="sqlvm-$randomIdentifier"

echo "create sql vm $sqlvm"
az sql vm create -n $sqlvm -g $resourceGroup -l $location --license-type DR

echo "list sql vms in resourceGroup $resourceGroup "
az sql vm list -g $resourceGroup

echo "get sql vm details for $sqlvm in resource group $resourceGroup"
az sql vm show --name $sqlvm --resource-group $resourceGroup
               
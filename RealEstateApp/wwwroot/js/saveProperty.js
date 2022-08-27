const upgrades = txtUpgrades.value.split(",");

console.log(txtUpgrades.value);
console.log(upgrades);

for (var i = 1; i < upgrades.length; i++) {
    console.log(upgrades[i]);
    let checkBox = document.querySelector(`#upgrade-${upgrades[i]}`);
    console.log(checkBox);
    checkBox.checked = true;
}

const addUpgrade = upId => {

    const txtUpgrades = document.querySelector("#txtUpgrades");
    const chck = document.querySelector(`#upgrade-${upId}`);

    let upList = txtUpgrades.value.split(",");

    if (chck.checked) {
        upList.push(upId);
    } else {
        let idx = upList.indexOf(""+upId);
        console.log(idx);
        if (idx > -1) {
            upList.splice(idx, 1);
        }
    }

    txtUpgrades.value = upList;
}
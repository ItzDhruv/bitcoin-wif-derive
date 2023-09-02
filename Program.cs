var mnemonicStr = Sharprompt.Prompt.Input<string>("Enter your mnemonic");

var nmenomic = new NBitcoin.Mnemonic(mnemonicStr);

var derivationPath = Sharprompt.Prompt.Input<string>("Enter your derivation path", defaultValue: "m/44'/60'/0'/0/0");

var network = NBitcoin.Network.GetNetwork(Sharprompt.Prompt.Select<string>("Select your network", new string[] { "regtest", "testnet", "mainnet" }));

var masterKey = nmenomic.DeriveExtKey();

var derivedKey = masterKey.Derive(new NBitcoin.KeyPath(derivationPath));

var privateKey = derivedKey.PrivateKey.GetWif(network);

var addressLegacy = derivedKey.PrivateKey.PubKey.GetAddress(NBitcoin.ScriptPubKeyType.Legacy, network);
var addressSegwit = derivedKey.PrivateKey.PubKey.GetAddress(NBitcoin.ScriptPubKeyType.Segwit, network);
var addressTaproot = derivedKey.PrivateKey.PubKey.GetAddress(NBitcoin.ScriptPubKeyType.TaprootBIP86, network);

System.Console.WriteLine($"Your legacy address is: {addressLegacy}");
System.Console.WriteLine($"Your segwit address is: {addressSegwit}");
System.Console.WriteLine($"Your taproot address is: {addressTaproot}");

Console.WriteLine($"Your wif is: {privateKey}");
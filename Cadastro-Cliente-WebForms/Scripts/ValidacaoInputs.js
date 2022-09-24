//bloqueador de caracteres
function BlockChar(field) {
    field = document.getElementById(field);
    let Letters = /[qwertyuiopasdfghjklçzxcvbnm\b]/g
    let Symbols = /[-[\]\/{}()*+?%&!?¨:.,;'"<>~=\\^$|#\b]/g
    document.getElementById(field.id).value = document.getElementById(field.id).value.replace(Letters, "");
    document.getElementById(field.id).value = document.getElementById(field.id).value.replace(Symbols, "");
}

//bloqueador de caracteres sem (.)
function BlockChar2(field) {
    field = document.getElementById(field);
    let Letters = /[qwertyuiopasdfghjklçzxcvbnm\b]/g
    let Symbols = /[-[\]\/{}()*+?%&!?¨:,;'´`"<>~=\\^$|#\b]/g
    document.getElementById(field.id).value = document.getElementById(field.id).value.replace(Letters, "");
    document.getElementById(field.id).value = document.getElementById(field.id).value.replace(Symbols, "");
}

//mascara telefone
function mascaraTelefone(campo) {

	function trata(valor, isOnBlur) {
		valor = valor.replace(/\D/g, "");
		valor = valor.replace(/^(\d{2})(\d)/g, "($1)$2");
		if (isOnBlur) {
			valor = valor.replace(/(\d)(\d{4})$/, "$1-$2");
		} else {
			valor = valor.replace(/(\d)(\d{3})$/, "$1-$2");
		}
		return valor;
	}

	campo.onkeypress = function (evt) {
		var code = (window.event) ? window.event.keyCode : evt.which;
		var valor = this.value
		if (code > 57 || (code < 48 && code != 8)) {
			return false;
		} else {
			this.value = trata(valor, false);
		}
	}

	campo.onblur = function () {

		var valor = this.value;
		if (valor.length < 13) {
			this.value = ""
		} else {
			this.value = trata(this.value, true);
		}
	}

	campo.maxLength = 14;
}

//mascara data
function mascaraData(val) {
    var pass = val.value;
    var expr = /[0123456789]/;

    for (i = 0; i < pass.length; i++) {
        // charAt -> retorna o caractere posicionado no índice especificado
        var lchar = val.value.charAt(i);
        var nchar = val.value.charAt(i + 1);

        if (i == 0) {
            // search -> retorna um valor inteiro, indicando a posição do inicio da primeira
            // ocorrência de expReg dentro de instStr. Se nenhuma ocorrencia for encontrada o método retornara -1
            // instStr.search(expReg);
            if ((lchar.search(expr) != 0) || (lchar > 3)) {
                val.value = "";
            }

        } else if (i == 1) {

            if (lchar.search(expr) != 0) {
                // substring(indice1,indice2)
                // indice1, indice2 -> será usado para delimitar a string
                var tst1 = val.value.substring(0, (i));
                val.value = tst1;
                continue;
            }

            if ((nchar != '/') && (nchar != '')) {
                var tst1 = val.value.substring(0, (i) + 1);

                if (nchar.search(expr) != 0)
                    var tst2 = val.value.substring(i + 2, pass.length);
                else
                    var tst2 = val.value.substring(i + 1, pass.length);

                val.value = tst1 + '/' + tst2;
            }

        } else if (i == 4) {

            if (lchar.search(expr) != 0) {
                var tst1 = val.value.substring(0, (i));
                val.value = tst1;
                continue;
            }

            if ((nchar != '/') && (nchar != '')) {
                var tst1 = val.value.substring(0, (i) + 1);

                if (nchar.search(expr) != 0)
                    var tst2 = val.value.substring(i + 2, pass.length);
                else
                    var tst2 = val.value.substring(i + 1, pass.length);

                val.value = tst1 + '/' + tst2;
            }
        }

        if (i >= 6) {
            if (lchar.search(expr) != 0) {
                var tst1 = val.value.substring(0, (i));
                val.value = tst1;
            }
        }
    }

    if (pass.length > 10)
        val.value = val.value.substring(0, 10);
    return true;
}

//mascara real
function formatarMoeda(field) {

    var elemento = document.getElementById(field);
    var valor = elemento.value;

    valor = valor + '';
    valor = parseInt(valor.replace(/[\D]+/g, ''));
    valor = valor + '';
    valor = valor.replace(/([0-9]{2})$/g, ",$1");

    if (valor.length > 6) {
        valor = valor.replace(/([0-9]{3}),([0-9]{2}$)/g, ".$1,$2");
    }

    elemento.value = valor;
}
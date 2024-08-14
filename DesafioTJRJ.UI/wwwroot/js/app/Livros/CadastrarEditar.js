$(document).ready(function () {

    $("#AnoPublicacao").mask("0000");
    $("#Edicao").mask("0000");
    $("#vlrPreco").mask('000.000.000.000.000,00', { reverse: true });

    $("form").on("submit", function (e) {
        var isValid = true;

        if ($("#tabelaAutores tbody tr[id^='autor_']").length === 0) {
            $("#autorError").text("Você deve adicionar pelo menos um Autor.");
            isValid = false;
        } else {
            $("#autorError").text("");
        }

        if ($("#tabelaAssuntos tbody tr[id^='assunto_']").length === 0) {
            $("#assuntoError").text("Você deve adicionar pelo menos um Assunto.");
            isValid = false;
        } else {
            $("#assuntoError").text("");
        }

        if ($("#tabelaPrecoFormaCompra tbody tr[id^='precoFormaCompra_']").length === 0) {
            $("#formaCompraError").text("Você deve adicionar pelo menos um Preço por Forma de Compra.");
            isValid = false;
        } else {
            $("#formaCompraError").text("");
        }

        if (!isValid) {
            e.preventDefault();
        }
    });

});

function addAutor() {

    var livroId = $('#CodL').val();
    var autorId = $('#novosAutores').val();
    var autorNome = $('#novosAutores option:selected').text();
    if (autorId) {
        var index = $('#tabelaAutores tbody tr').length;
        $('#tabelaAutores tbody').append(`
            <tr id="autor_${autorId}">
                <td>${autorNome}</td>
                <td class="actionColumn">
                    <button type="button" class="btn btn-danger" onclick="removeAutor(${autorId}, ${index})">Remover</button>
                    <input type="hidden" name="Autores[${index}].CodL" value="${livroId}" />
                    <input type="hidden" name="Autores[${index}].CodAu" value="${autorId}" />
                    <input type="hidden" name="Autores[${index}].Nome" value="${autorNome}" />
                </td>
            </tr>
        `);
        $('#novosAutores option:selected').remove();
        $('#autorError').text('');

        sortOptions();
    } else {
        $('#autorError').text('Selecione um autor para adicionar.');
    }
}

function removeAutor(autorId, autorNome) {

    var row = $(`#autor_${autorId}`);
    var autorNome = row.find('td').first().text();
    $('#novosAutores').append(`<option value="${autorId}">${autorNome}</option>`);
    row.remove();
    $('#autorError').text('');
    sortOptions();

    $('#tabelaAutores tbody tr').each(function (newIndex) {
        $(this).find('input[type="hidden"]').each(function () {
            var name = $(this).attr('name');
            if (name) {
                var novoName = name.replace(/\[\d+\]/, '[' + newIndex + ']');
                $(this).attr('name', novoName);
            }
        });
    });
}

function addAssunto() {

    var livroId = $('#CodL').val();
    var assuntoId = $('#novosAssuntos').val();
    var assuntoDescricao = $('#novosAssuntos option:selected').text();
    if (assuntoId) {
        var index = $('#tabelaAssuntos tbody tr').length;
        $('#tabelaAssuntos tbody').append(`
            <tr id="assunto_${assuntoId}">
                <td>${assuntoDescricao}</td>
                <td class="actionColumn">
                    <button type="button" class="btn btn-danger" onclick="removeAssunto(${assuntoId}, ${index})">Remover</button>
                    <input type="hidden" name="Assuntos[${index}].CodL" value="${livroId}" />
                    <input type="hidden" name="Assuntos[${index}].CodAs" value="${assuntoId}" />
                    <input type="hidden" name="Assuntos[${index}].Descricao" value="${assuntoDescricao}" />
                </td>
            </tr>
        `);
        $('#novosAssuntos option:selected').remove();
        $('#assuntoError').text('');

        sortOptions();
    } else {
        $('#assuntoError').text('Selecione um assunto para adicionar.');
    }
}

function removeAssunto(assuntoId, assuntoDescricao) {
    var row = $(`#assunto_${assuntoId}`);
    var assuntoDescricao = row.find('td').first().text();
    $('#novosAssuntos').append(`<option value="${assuntoId}">${assuntoDescricao}</option>`);
    row.remove();
    $('#assuntoError').text('');

    $('#tabelaAssuntos tbody tr').each(function (newIndex) {
        $(this).find('input[type="hidden"]').each(function () {
            var name = $(this).attr('name');
            if (name) {
                var novoName = name.replace(/\[\d+\]/, '[' + newIndex + ']');
                $(this).attr('name', novoName);
            }
        });
    });
}

function addPrecoFormaCompra() {

    var livroId = $('#CodL').val();
    var formaCompraId = $('#novosFormaCompra').val();
    var formaCompraDescricao = $('#novosFormaCompra option:selected').text();
    var precoValor = $('#vlrPreco').val();
    var precoValorFormatado = precoValor.replace('.', ',').replace(/\B(?=(\d{3})+(?!\d))/g, '.');

    if (formaCompraId && precoValor) {
        var index = $('#tabelaPrecoFormaCompra tbody tr').length;
        $('#tabelaPrecoFormaCompra tbody').append(`
            <tr id="precoFormaCompra_${formaCompraId}">
                <td>${formaCompraDescricao}</td>
                <td>R$ ${precoValorFormatado}</td>
                <td class="actionColumn">
                    <button type="button" class="btn btn-danger" onclick="removePrecoFormaCompra(${formaCompraId}, ${index})">Remover</button>
                    <input type="hidden" name="PrecosFormaCompra[${index}].CodL" value="${livroId}" />
                    <input type="hidden" name="PrecosFormaCompra[${index}].CodFormaCompra" value="${formaCompraId}" />
                    <input type="hidden" name="PrecosFormaCompra[${index}].FormaCompra" value="${formaCompraDescricao}" />
                    <input type="hidden" name="PrecosFormaCompra[${index}].Preco" value="${precoValor}" />
                </td>
            </tr>
        `);
        $('#novosFormaCompra option:selected').remove();
        $('#formaCompraError').text('');
        $('#vlrPreco').val('');
        sortOptions();
    } else {
        $('#formaCompraError').text('Informe um preço e uma forma de compra para adicionar.');
    }
}

function removePrecoFormaCompra(formaCompraId, formaCompraDescricao) {

    var row = $(`#precoFormaCompra_${formaCompraId}`);
    var formaCompraDescricao = row.find('td').first().text();
    $('#novosFormaCompra').append(`<option value="${formaCompraId}">${formaCompraDescricao}</option>`);
    row.remove();
    $('#formaCompraError').text('');

    $('#tabelaPrecoFormaCompra tbody tr').each(function (newIndex) {
        $(this).find('input[type="hidden"]').each(function () {
            var name = $(this).attr('name');
            if (name) {
                var novoName = name.replace(/\[\d+\]/, '[' + newIndex + ']');
                $(this).attr('name', novoName);
            }
        });
    });
}

function sortOptions() {
    $("#novosAutores, #novosAssuntos, #novosFormaCompra").each(function () {
        var $options = $(this).find('option');
        $options.sort(function (a, b) {
            return $(a).text().localeCompare($(b).text());
        });
        $(this).empty().append($options);
    });
}
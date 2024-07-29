$(document).ready(function () {

    $("#AnoPublicacao").mask("0000");
    $("#Edicao").mask("0000");
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
                <td>
                    <button type="button" class="btn btn-danger" onclick="removeAutor(${autorId}, ${index})">Remover</button>
                </td>
                <input type="hidden" name="Autores[${index}].CodL" value="${livroId}" />
                <input type="hidden" name="Autores[${index}].CodAu" value="${autorId}" />
                <input type="hidden" name="Autores[${index}].Nome" value="${autorNome}" />
            </tr>
        `);
        $('#novosAutores option:selected').remove();
        $('#autorError').text('');

        sortOptions();
    } else {
        $('#autorError').text('Selecione um autor para adicionar.');
    }
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
                <td>
                    <button type="button" class="btn btn-danger" onclick="removeAssunto(${assuntoId}, ${index})">Remover</button>
                </td>
                <input type="hidden" name="Assuntos[${index}].CodL" value="${livroId}" />
                <input type="hidden" name="Assuntos[${index}].CodAs" value="${assuntoId}" />
                <input type="hidden" name="Assuntos[${index}].Descricao" value="${assuntoDescricao}" />
            </tr>
        `);
        $('#novosAssuntos option:selected').remove();
        $('#assuntoError').text('');

        sortOptions();
    } else {
        $('#assuntoError').text('Selecione um assunto para adicionar.');
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

function sortOptions() {
    $("#novosAutores, #novosAssuntos").each(function () {
        var $options = $(this).find('option');
        $options.sort(function (a, b) {
            return $(a).text().localeCompare($(b).text());
        });
        $(this).empty().append($options);
    });
}